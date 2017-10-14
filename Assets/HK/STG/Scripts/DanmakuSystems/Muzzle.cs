using System;
using HK.STG.Events;
using HK.STG.ObjectPools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    public sealed class Muzzle : MonoBehaviour
    {
        [SerializeField]
        private MuzzleParameter parameter;

        private Transform cachedTransform;

        private int coolTime;

        private int parameterIndex;

        private IDisposable fireStream;

        void Awake()
        {
            this.cachedTransform = this.transform;
            if (this.parameter != null)
            {
                this.coolTime = this.parameter.InitialCoolTime;
            }
        }

        void Update()
        {
            this.coolTime--;
        }

        public void Attach(IMessageBroker broker, MuzzleParameter parameter)
        {
            this.parameter = parameter;
            this.Attach(broker);
        }
        
        public void Attach(IMessageBroker broker)
        {
            this.parameterIndex = 0;
            this.coolTime = this.parameter.InitialCoolTime;
            
            if (this.fireStream != null)
            {
                return;
            }
            
            this.fireStream = broker.Receive<Fire>()
                .Where(_ => this.coolTime <= 0)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Fire();
                })
                .AddTo(this);
        }
        
        private void Fire()
        {
            var currentParameter = this.CurrentParameter;
            var bullet = BulletPool.Rent(currentParameter.BulletPrefab);
            bullet.Setup(null, this.cachedTransform, currentParameter.Speed);
            this.coolTime = currentParameter.CoolTime;
            this.parameterIndex++;
            if (this.parameter.Parameters.Count <= this.parameterIndex)
            {
                this.parameterIndex = 0;
            }
        }

        private MuzzleParameter.Parameter CurrentParameter
        {
            get { return this.parameter.Parameters[this.parameterIndex]; }
        }
    }
}

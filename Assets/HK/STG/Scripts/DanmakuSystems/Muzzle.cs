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
            this.coolTime++;
        }

        public void Attach(IMessageBroker broker, MuzzleParameter parameter)
        {
            this.parameter = parameter;
            this.Attach(broker);
        }
        
        public void Attach(IMessageBroker broker)
        {
            if (this.fireStream != null)
            {
                this.fireStream.Dispose();
            }
            this.fireStream = broker.Receive<Fire>()
                .Where(_ => this.coolTime >= this.parameter.CoolTime)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Fire();
                })
                .AddTo(this);
        }
        
        private void Fire()
        {
            var bullet = BulletPool.Rent(this.parameter.BulletPrefab);
            bullet.Setup(null, this.cachedTransform, this.parameter.Speed);
            this.coolTime = 0;
        }
    }
}

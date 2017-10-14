using System;
using HK.STG.Events;
using HK.STG.GameSystems;
using HK.STG.ObjectPools;
using NUnit.Framework.Constraints;
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

        private int loop;

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
            this.loop = 0;
            this.coolTime = this.parameter.InitialCoolTime;
            
            if (this.fireStream != null)
            {
                return;
            }
            
            this.fireStream = broker.Receive<Fire>()
                .Where(_ => this.CanFire)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Fire();
                })
                .AddTo(this);
        }
        
        private void Fire()
        {
            var currentParameter = this.CurrentParameter;
            if (currentParameter.LookAtPlayer)
            {
                this.cachedTransform.up = GameWorld.Instance.Player.CachedTransform.position - this.cachedTransform.position;
            }
            this.ChangeAngle(currentParameter);
            var bullet = BulletPool.Rent(currentParameter.BulletPrefab);
            bullet.Setup(null, this.cachedTransform, currentParameter.Speed.Random);
            this.coolTime = currentParameter.CoolTime;
            this.parameterIndex++;
            if (this.parameter.Parameters.Count <= this.parameterIndex)
            {
                this.parameterIndex = 0;
                this.loop++;
            }
        }

        private bool CanFire
        {
            get
            {
                if (this.parameter.Loop > 0 && this.loop >= this.parameter.Loop)
                {
                    return false;
                }
                
                return this.coolTime <= 0;
            }
        }

        private void ChangeAngle(MuzzleParameter.Parameter parameter)
        {
            switch (parameter.AngleModifyType)
            {
                case AngleModifyType.None:
                    break;
                case AngleModifyType.Set:
                    this.cachedTransform.rotation = Quaternion.AngleAxis(parameter.Angle.Random, Vector3.forward);
                    break;
                case AngleModifyType.Add:
                    this.cachedTransform.rotation *= Quaternion.AngleAxis(parameter.Angle.Random, Vector3.forward);
                    break;
            }
        }

        private MuzzleParameter.Parameter CurrentParameter
        {
            get { return this.parameter.Parameters[this.parameterIndex]; }
        }
    }
}

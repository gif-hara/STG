using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.STG.DanmakuSystems
{
    public sealed class BulletUpdater : MonoBehaviour
    {
        private static BulletUpdater instance;

        private List<Bullet> bullets = new List<Bullet>();
        
        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;
            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.UpdateBullet();
                });
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }

        private void UpdateBullet()
        {
            foreach (var bullet in this.bullets)
            {
                bullet.UpdateFromUpdater();
            }
        }

        public static void Add(Bullet bullet)
        {
            instance.bullets.Add(bullet);
        }

        public static void Remove(Bullet bullet)
        {
            #if UNITY_EDITOR
            if (instance == null)
            {
                return;
            }
            #endif
            instance.bullets.Remove(bullet);
        }
    }
}

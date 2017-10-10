using System.Collections.Generic;
using HK.STG.DanmakuSystems;
using UniRx.Toolkit;
using UnityEngine;

namespace HK.STG.ObjectPools
{
    public sealed class BulletPool : ObjectPool<Bullet>
    {
        private readonly Bullet bullet;
        
        public BulletPool(Bullet bullet)
        {
            this.bullet = bullet;
        }

        protected override Bullet CreateInstance()
        {
            var instance = Object.Instantiate(this.bullet);
            instance.Pool = this;
            return instance;
        }

        private static Dictionary<Bullet, BulletPool> cachedPools = new Dictionary<Bullet, BulletPool>();
        public static Bullet Rent(Bullet original)
        {
            BulletPool pool;
            if (!cachedPools.TryGetValue(original, out pool))
            {
                pool = new BulletPool(original);
                cachedPools.Add(original, pool);
            }

            return pool.Rent();
        }
    }
}

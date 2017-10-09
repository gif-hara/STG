using HK.STG.CharacterController;
using HK.STG.Extensions;
using HK.STG.ObjectPools;
using UniRx;
using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    public sealed class Muzzle : MonoBehaviour
    {
        [SerializeField]
        private Character bulletPrefab;

        [SerializeField]
        private float speed;

        [SerializeField]
        private int waitFrame;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
            Observable.IntervalFrame(this.waitFrame)
                .Subscribe(_ => this.Fire());
        }
        
        public void Fire()
        {
            var bullet = CharacterPool.Rent(this.bulletPrefab);
            bullet.SetupAsBullet(this.cachedTransform, this.speed);
        }
    }
}

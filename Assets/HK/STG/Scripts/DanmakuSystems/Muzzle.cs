using HK.STG.ObjectPools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    public sealed class Muzzle : MonoBehaviour
    {
        [SerializeField]
        private Bullet bulletPrefab;

        [SerializeField]
        private float speed;

        [SerializeField]
        private int waitFrame;

        [SerializeField]
        private float rotateAxis;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
            Observable.IntervalFrame(this.waitFrame)
                .Subscribe(_ => this.Fire());
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cachedTransform.rotation *= Quaternion.AngleAxis(_this.rotateAxis, Vector3.forward);
                });
        }
        
        public void Fire()
        {
            var bullet = BulletPool.Rent(this.bulletPrefab);
            bullet.Setup(null, this.cachedTransform, this.speed);
        }
    }
}

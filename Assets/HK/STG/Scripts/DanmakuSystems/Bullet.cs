using HK.STG.CharacterControllers;
using HK.STG.Events;
using HK.STG.GameSystems;
using HK.STG.ObjectPools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using CharacterController = HK.STG.CharacterControllers.CharacterController;

namespace HK.STG.DanmakuSystems
{
    /// <summary>
    /// <see cref="Character"/>を弾として扱うクラス
    /// </summary>
    [RequireComponent(typeof(Character))]
    public sealed class Bullet : MonoBehaviour, CharacterController
    {
        private float speed;

        public Character Character { set; get; }
        
        public BulletPool Pool { set; get; }
        
        void Awake()
        {
            this.Character = this.GetComponent<Character>();
            this.Character.Broker.Receive<ScreenOut>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.PoolOrDestroy();
                })
                .AddTo(this);
        }

        void OnEnable()
        {
            BulletUpdater.Add(this);
        }

        void OnDisable()
        {
            BulletUpdater.Remove(this);
        }

        public void UpdateFromUpdater()
        {
            this.Character.Broker.Publish(Move.GetCached(this.Character.CachedTransform.up * this.speed));
        }
        
        public void PoolOrDestroy()
        {
            if (this.Pool == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.Pool.Return(this);
            }
        }

        public void Setup(Transform parent, Transform muzzle, float speed)
        {
            this.Character.CachedTransform.position = muzzle.position;
            this.Character.CachedTransform.rotation = muzzle.rotation;
            this.speed = speed;
            GameWorld.AddScreenOutMonitor(this.Character.Broker, this.Character.CachedTransform, Vector2.zero);
        }
    }
}

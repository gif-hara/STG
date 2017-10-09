using HK.STG.Events;
using HK.STG.ObjectPools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.STG.CharacterController
{
    public sealed class Character : MonoBehaviour
    {
        public IMessageBroker Broker { private set; get; }
        
        public Transform CachedTransform { private set; get; }
        
        public CharacterPool Pool { set; get; }

        void Awake()
        {
            this.Broker = new MessageBroker();
            this.CachedTransform = this.transform;
            
            this.Broker.Receive<Move>()
                .Subscribe(this.OnMove)
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .Subscribe(this.OnHit)
                .AddTo(this);
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

        private void OnMove(Move move)
        {
            this.CachedTransform.position += move.Velocity;
        }

        private void OnHit(Collider2D collider)
        {
            this.Broker.Publish(Hit.Get(collider));
        }
    }
}

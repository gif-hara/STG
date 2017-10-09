using HK.STG.Events;
using UniRx;
using UnityEngine;

namespace HK.STG.CharacterController
{
    public sealed class Character : MonoBehaviour
    {
        public IMessageBroker Broker { private set; get; }
        
        public Transform CachedTransform { private set; get; }

        void Awake()
        {
            this.Broker = new MessageBroker();
            this.CachedTransform = this.transform;
            
            this.Broker.Receive<Move>()
                .Subscribe(this.OnMove)
                .AddTo(this);
        }

        private void OnMove(Move move)
        {
            this.CachedTransform.position += move.Velocity;
        }
    }
}

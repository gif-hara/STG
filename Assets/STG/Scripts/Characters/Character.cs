using UniRx;
using UnityEngine;

namespace HK.STG.CharacterController
{
    public sealed class Character : MonoBehaviour
    {
        public IMessageBroker Broker { private set; get; }

        void Awake()
        {
            this.Broker = new MessageBroker();
        }
    }
}

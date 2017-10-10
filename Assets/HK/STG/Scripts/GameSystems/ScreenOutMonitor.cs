using UniRx;
using UnityEngine;

namespace HK.STG.GameSystems
{
    public sealed class ScreenOutMonitor
    {
        public IMessageBroker Broker { private set; get; }
        
        public Transform Target { private set; get; }
        
        public Vector2 Size { private set; get; }

        public ScreenOutMonitor(IMessageBroker broker, Transform target, Vector2 size)
        {
            this.Broker = broker;
            this.Target = target;
            this.Size = size;
        }
    }
}

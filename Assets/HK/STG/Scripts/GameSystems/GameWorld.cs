using HK.STG.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.STG.GameSystems
{
    public sealed class GameWorld : MonoBehaviour
    {
        public static GameWorld Instance { private set; get; }

        [SerializeField]
        private Vector2 worldRange;

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }
        
#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(this.worldRange.x * 2, this.worldRange.y * 2, 0.0f));
        }
#endif

        public static IObservable<ScreenOut> ReceiveScreenOut(IMessageBroker broker, Transform transform, Vector2 size)
        {
            transform.UpdateAsObservable()
                .TakeUntilDisable(transform.gameObject)
                .SubscribeWithState3(Instance, broker, transform, (_, _instance, _broker, _transform) =>
                {
                    var r = _instance.worldRange + size;
                    var p = _transform.position;
                    if (p.x > r.x || p.x < -r.x || p.y > r.y || p.y < -r.y)
                    {
                        _broker.Publish(ScreenOut.Get());
                    }
                });
            return broker.Receive<ScreenOut>();
        }
    }
}

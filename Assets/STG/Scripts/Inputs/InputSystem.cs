using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.STG.InputSystems
{
    public sealed class InputSystem : MonoBehaviour
    {
        public static InputSystem Instance { private set; get; }
        
        private Subject<Vector2> direction = new Subject<Vector2>();
        
        private Subject<bool> decision = new Subject<bool>();
        
        public static IObservable<Vector2> DirectionAsObservable()
        {
            return Instance.direction;
        }

        public static IObservable<bool> DecisionAsObservable()
        {
            return Instance.decision;
        }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
            
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.UpdateDirection();
                    _this.UpdateDecision();
                });
        }

        void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }

        private void UpdateDirection()
        {
            var inputDirection = new Vector2(
                Input.GetKey(KeyCode.LeftArrow) ? -1.0f : Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0.0f,
                Input.GetKey(KeyCode.DownArrow) ? -1.0f : Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0.0f
            );
            this.direction.OnNext(inputDirection);
        }

        private void UpdateDecision()
        {
            this.decision.OnNext(Input.GetKey(KeyCode.Z));
        }
    }
}

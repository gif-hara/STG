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
        
        private Vector2 cachedInputDirection = new Vector2();

        public static IObservable<Vector2> DirectionAsObservable()
        {
            return Instance.direction;
        }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
            
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cachedInputDirection.x =
                        Input.GetKey(KeyCode.LeftArrow) ? -1.0f :
                            Input.GetKey(KeyCode.RightArrow) ? 1.0f :
                                0.0f;
                    _this.cachedInputDirection.y =
                        Input.GetKey(KeyCode.DownArrow) ? -1.0f :
                            Input.GetKey(KeyCode.UpArrow) ? 1.0f :
                                0.0f;
                    _this.direction.OnNext(_this.cachedInputDirection);
                });
        }

        void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }
    }
}

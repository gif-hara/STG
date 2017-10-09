using HK.STG.InputSystems;
using UniRx;
using UnityEngine;

namespace HK.STG.CharacterController
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        void Awake()
        {
            InputSystem.DirectionAsObservable()
                .SubscribeWithState(this, (d, _this) =>
                {
                    var position = _this.transform.position;
                    var direction = d.normalized;
                    position.x += direction.x;
                    position.y += direction.y;
                    _this.transform.position = position;
                })
                .AddTo(this);
        }
    }
}

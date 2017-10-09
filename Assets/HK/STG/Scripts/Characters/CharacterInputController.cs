using HK.STG.Events;
using HK.STG.InputSystems;
using UniRx;
using UnityEngine;

namespace HK.STG.CharacterController
{
    [RequireComponent(typeof(Character))]
    public sealed class CharacterInputController : MonoBehaviour
    {
        void Awake()
        {
            var character = this.GetComponent<Character>();
            InputSystem.DirectionAsObservable()
                .SubscribeWithState(this, (d, _this) =>
                {
                    d = d.normalized;
                    character.Broker.Publish(Move.Get(new Vector3(d.x, d.y, 0.0f)));
                })
                .AddTo(this);
        }
    }
}

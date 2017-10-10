using HK.STG.Events;
using HK.STG.InputSystems;
using UniRx;
using UnityEngine;

namespace HK.STG.CharacterControllers
{
    [RequireComponent(typeof(Character))]
    public sealed class CharacterInputController : MonoBehaviour, CharacterController
    {
        public Character Character { set; get; }

        void Awake()
        {
            this.Character = this.GetComponent<Character>();
            InputSystem.DirectionAsObservable()
                .SubscribeWithState(this, (d, _this) =>
                {
                    d = d.normalized;
                    _this.Character.Broker.Publish(Move.GetCache(new Vector3(d.x, d.y, 0.0f)));
                })
                .AddTo(this);
            InputSystem.DecisionAsObservable()
                .Where(b => b)
                .SubscribeWithState(this, (b, _this) =>
                {
                    _this.Character.Broker.Publish(Fire.GetCache());
                })
                .AddTo(this);
        }
    }
}

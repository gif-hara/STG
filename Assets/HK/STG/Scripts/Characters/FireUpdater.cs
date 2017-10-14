using HK.STG.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.STG.CharacterControllers
{
    [RequireComponent(typeof(Character))]
    public sealed class FireUpdater : MonoBehaviour
    {
        void Awake()
        {
            var character = this.GetComponent<Character>();
            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(character, (_, _chatacter) =>
                {
                    _chatacter.Broker.Publish(Fire.GetCache());
                })
                .AddTo(character);
        }
    }
}

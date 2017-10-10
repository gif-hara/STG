using HK.STG.Events;
using UnityEngine;
using UniRx;

namespace HK.STG.CharacterControllers
{
    [RequireComponent(typeof(Character))]
    public sealed class PlayerHitController : MonoBehaviour
    {
        void Start()
        {
//            var character = this.GetComponent<Character>();
//            character.Broker.Receive<Hit>()
//                .Subscribe(_ => Debug.Log("Miss!"))
//                .AddTo(this);
        }
    }
}

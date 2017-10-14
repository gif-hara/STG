using HK.Framework.EventSystems;
using HK.STG.CharacterControllers;
using HK.STG.Events;
using UnityEngine;

namespace HK.STG.GameSystems
{
    public sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private Character prefab;

        void Start()
        {
            var player = Instantiate(this.prefab);
            player.CachedTransform.position = this.transform.position;
            UniRxEvent.GlobalBroker.Publish(PlayerSpawned.Get(player));
        }
    }
}

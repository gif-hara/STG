using HK.STG.CharacterControllers;
using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    [RequireComponent(typeof(Character))]
    public sealed class MuzzleAttacher : MonoBehaviour, CharacterControllers.CharacterController
    {
        public Character Character { set; get; }

        void Awake()
        {
            this.Character = this.GetComponent<Character>();
        }

        void Start()
        {
            var muzzles = this.GetComponentsInChildren<Muzzle>();
            foreach (var muzzle in muzzles)
            {
                muzzle.Attach(this.Character.Broker);
            }
        }
    }
}

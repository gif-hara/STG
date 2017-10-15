using HK.STG.CharacterControllers;
using HK.STG.EnemyStateControllers;
using UnityEngine;

namespace HK.STG.GameSystems
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private StateList debugStateList;

        [SerializeField]
        private int initialIndex;

        [SerializeField]
        private Character debugEnemy;

        [SerializeField]
        private Vector3 position;

        void Start()
        {
            var enemy = Instantiate(this.debugEnemy, this.position, Quaternion.AngleAxis(180, Vector3.forward));
            EnemyStateMachine.Attach(enemy, this.debugStateList, this.initialIndex);
        }
    }
}

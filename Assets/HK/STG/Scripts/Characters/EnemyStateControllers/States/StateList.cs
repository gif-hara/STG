using System;
using System.Collections.Generic;
using UnityEngine;

namespace HK.STG.EnemyStateControllers
{
    [CreateAssetMenu(menuName = "HK/STG/EnemyStateController/StateList")]
    public sealed class StateList : ScriptableObject
    {
        [SerializeField]
        private List<EnemyStates> list;
        public List<EnemyStates> Get { get { return this.list; } }

        [Serializable]
        public class EnemyStates
        {
            [SerializeField]
            private List<EnemyState> states = new List<EnemyState>();
            public List<EnemyState> States { get { return this.states; } }

            [SerializeField]
            private int nextStateIndex;
            public int NextStateIndex { get { return this.nextStateIndex; } set { this.nextStateIndex = value; } }

            [SerializeField]
            private int coolTime;
            public int CoolTime { get { return this.coolTime; } set { this.coolTime = value; } }

            public EnemyStates NextState(StateList stateList)
            {
                return stateList.Get[this.nextStateIndex];
            }
        }
    }
}

using System;
using HK.STG.CharacterControllers;
using UnityEngine;

namespace HK.STG.EnemyStateControllers
{
    public abstract class EnemyState : ScriptableObject
    {
        public abstract void OnStart(EnemyStateMachine stateMachine, Character character);

        public abstract void OnExit();
        
        public abstract StateType Type { get; }
    }
}

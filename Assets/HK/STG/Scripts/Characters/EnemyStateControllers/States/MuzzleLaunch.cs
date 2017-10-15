using System;
using System.Collections.Generic;
using HK.STG.CharacterControllers;
using HK.STG.DanmakuSystems;
using HK.STG.Events;
using UniRx;
using UnityEngine;

namespace HK.STG.EnemyStateControllers
{
    [CreateAssetMenu(menuName = "HK/STG/EnemyStateController/State/MuzzleLaunch")]
    public sealed class MuzzleLaunch : EnemyState
    {
        [SerializeField]
        private List<MuzzleAttach> muzzleAttaches = new List<MuzzleAttach>();
        public List<MuzzleAttach> MuzzleAttaches { get { return this.muzzleAttaches; } }

        private readonly List<Muzzle> useMuzzles = new List<Muzzle>();

        private IDisposable completedStream;
        
        public override void OnStart(EnemyStateMachine stateMachine, Character character)
        {
            foreach (var muzzleAttach in muzzleAttaches)
            {
                var muzzle = character.Muzzles[muzzleAttach.MuzzleIndex];
                muzzle.Attach(character.Broker, muzzleAttach.Parameter);
                this.useMuzzles.Add(muzzle);
            }
            this.completedStream = character.Broker.Receive<CompletedMuzzle>()
                .SubscribeWithState2(this, stateMachine, (c, _this, _stateMachine) =>
                {
                    _this.useMuzzles.Remove(c.Muzzle);
                    if (_this.useMuzzles.Count <= 0)
                    {
                        _stateMachine.CompleteState();
                        _this.completedStream.Dispose();
                    }
                })
                .AddTo(character);
        }

        public override void OnExit()
        {
        }
        
        public override StateType Type { get{ return StateType.MuzzleLaunch; } }

        [Serializable]
        public class MuzzleAttach
        {
            [SerializeField]
            private int muzzleIndex;
            public int MuzzleIndex { set { this.muzzleIndex = value; } get { return this.muzzleIndex; } }

            [SerializeField]
            private MuzzleParameter parameter;
            public MuzzleParameter Parameter { set { this.parameter = value; } get { return this.parameter; } }
        }
    }
}

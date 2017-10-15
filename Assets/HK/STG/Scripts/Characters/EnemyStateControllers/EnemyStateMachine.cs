using HK.STG.CharacterControllers;
using UniRx;

namespace HK.STG.EnemyStateControllers
{
    public sealed class EnemyStateMachine
    {
        private Character character;
        
        private StateList stateList;

        private StateList.EnemyStates currentStates;

        private int completedCount = 0;

        public static void Attach(Character character, StateList stateList, int initialIndex)
        {
            var instance = new EnemyStateMachine();
            instance.Setup(character, stateList, initialIndex);
        }
        
        private EnemyStateMachine()
        {
        }

        private void Setup(Character character, StateList stateList, int initialIndex)
        {
            this.character = character;
            this.stateList = stateList;
            this.currentStates = stateList.Get[initialIndex];
            this.Start();
        }

        public void Start()
        {
            this.completedCount = 0;
            foreach (var enemyState in this.currentStates.States)
            {
                enemyState.OnStart(this, this.character);
            }
        }

        public void CompleteState()
        {
            ++this.completedCount;
            if (this.currentStates.States.Count > this.completedCount)
            {
                return;
            }

            Observable.TimerFrame(this.currentStates.CoolTime)
                .SubscribeWithState(this, (_, _this) =>
                {
                    foreach (var enemyState in _this.currentStates.States)
                    {
                        enemyState.OnExit();
                    }

                    _this.currentStates = _this.currentStates.NextState(_this.stateList);
                    _this.Start();
                })
                .AddTo(this.character);
        }
    }
}

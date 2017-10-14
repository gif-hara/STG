using System.Collections.Generic;
using HK.Framework.EventSystems;
using HK.STG.CharacterControllers;
using HK.STG.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.STG.GameSystems
{
    public sealed class GameWorld : MonoBehaviour
    {
        public static GameWorld Instance { private set; get; }

        [SerializeField]
        private Vector2 worldRange;

        [SerializeField]
        private GameObject hoge;

        public Character Player { private set; get; }

        /// <summary>
        /// 画面外検出を行うオブジェクト
        /// </summary>
        private List<ScreenOutMonitor> screenOutMonitors = new List<ScreenOutMonitor>();

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            UniRxEvent.GlobalBroker.Receive<PlayerSpawned>()
                .SubscribeWithState(this, (p, _this) =>
                {
                    _this.Player = p.Player;
                })
                .AddTo(this);

//            this.UpdateAsObservable()
//                .Where(this.CanUpdate)
//                .SubscribeWithState(this, (_, _this) =>
//                {
//                    _this.MonitoringScreenOut();
//                });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var randomPosition = new Vector2(Random.Range(-this.worldRange.x, this.worldRange.x), Random.Range(-this.worldRange.y, this.worldRange.y));
                Instantiate(hoge).transform.position = randomPosition;
            }
            this.MonitoringScreenOut();
        }

        private void OnGUI()
        {
            GUILayout.Label(Character.Instances.Count.ToString());
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(this.worldRange.x * 2, this.worldRange.y * 2, 0.0f));
        }
#endif

        private void MonitoringScreenOut()
        {
            for (int i = 0; i < this.screenOutMonitors.Count;)
            {
                var screenOutMonitor = this.screenOutMonitors[i];
                var r = this.worldRange + screenOutMonitor.Size;
                var p = screenOutMonitor.Target.position;
                if (p.x > r.x || p.x < -r.x || p.y > r.y || p.y < -r.y)
                {
                    screenOutMonitor.Broker.Publish(ScreenOut.Get());
                    this.screenOutMonitors.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }

        private bool CanUpdate(Unit unit)
        {
            return this.isActiveAndEnabled;
        }

        public static void AddScreenOutMonitor(IMessageBroker broker, Transform transform, Vector2 size)
        {
            Instance.screenOutMonitors.Add(new ScreenOutMonitor(broker, transform, size));
        }
    }
}

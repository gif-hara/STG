using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using HK.STG.DanmakuSystems;
using HK.STG.Events;
using HK.STG.ObjectPools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.STG.CharacterControllers
{
    public sealed class Character : MonoBehaviour
    {
        private static readonly List<Character> instances = new List<Character>();

        [SerializeField]
        private List<Muzzle> muzzles;
        public List<Muzzle> Muzzles { get { return this.muzzles; } }
        
        public static List<Character> Instances { get { return instances; } }
        
        private IMessageBroker broker = new MessageBroker();
        public IMessageBroker Broker { get { return this.broker; } }
        
        public Transform CachedTransform { private set; get; }

#if UNITY_EDITOR
        [ContextMenu("Init Muzzles")]
        void InitMuzzles()
        {
            this.muzzles = new List<Muzzle>(this.GetComponentsInChildren<Muzzle>());
        }
#endif
        
        void Awake()
        {
            this.CachedTransform = this.transform;
            
            this.Broker.Receive<Move>()
                .Subscribe(this.OnMove)
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .Subscribe(this.OnHit)
                .AddTo(this);
            
            Assert.AreEqual(this.GetComponentsInChildren<Muzzle>().Length, this.Muzzles.Count, "Muzzleの数が一致しません");
        }

        void OnEnable()
        {
            instances.Add(this);
        }

        void OnDisable()
        {
            instances.Remove(this);
        }

        private void OnMove(Move move)
        {
            this.CachedTransform.position += move.Velocity;
        }

        private void OnHit(Collider2D collider)
        {
            this.Broker.Publish(Hit.Get(collider));
        }
    }
}

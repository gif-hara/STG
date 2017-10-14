using System;
using System.Collections.Generic;
using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    [CreateAssetMenu(menuName = "HK/STG/DanmakuSystem/MuzzleParameter")]
    public sealed class MuzzleParameter : ScriptableObject
    {
        [SerializeField]
        private int initialCoolTime;
        public int InitialCoolTime { get { return this.initialCoolTime; } }

        [SerializeField]
        private List<Parameter> parameters;

        public List<Parameter> Parameters
        {
            get { return this.parameters; }
        }

        [Serializable]
        public class Parameter
        {
            [SerializeField]
            private Bullet bulletPrefab;
            public Bullet BulletPrefab { get { return this.bulletPrefab; } }

            [SerializeField]
            private float speed;
            public float Speed { get { return this.speed; } }

            [SerializeField]
            private int coolTime;
            public int CoolTime { get { return this.coolTime; } }
        }
    }
}

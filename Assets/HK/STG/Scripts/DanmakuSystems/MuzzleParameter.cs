using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    [CreateAssetMenu(menuName = "HK/STG/DanmakuSystem/MuzzleParameter")]
    public sealed class MuzzleParameter : ScriptableObject
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

        [SerializeField]
        private int initialCoolTime;
        public int InitialCoolTime { get { return this.initialCoolTime; } }
    }
}

using HK.STG.CharacterController;
using HK.STG.ObjectPools;
using UnityEngine;

namespace HK.STG.DanmakuSystems
{
    public sealed class Muzzle : MonoBehaviour
    {
        [SerializeField]
        private Character bulletPrefab;
        
        public void Fire()
        {
            var bullet = CharacterPool.Rent(this.bulletPrefab);
            bullet.SetupAsBullet();
        }
    }
}

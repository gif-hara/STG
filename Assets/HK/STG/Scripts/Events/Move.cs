using HK.Framework.EventSystems;
using UnityEngine;

namespace HK.STG.Events
{
    public sealed class Move : UniRxEvent<Move, Vector3>
    {
        private static Move cache = new Move();
        
        public Vector3 Velocity { get { return this.param1; } }

        public static Move GetCache(Vector3 velocity)
        {
            cache.param1 = velocity;

            return cache;
        }
    }
}

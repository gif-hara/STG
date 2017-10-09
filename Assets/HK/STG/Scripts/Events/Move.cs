using HK.Framework.EventSystems;
using UnityEngine;

namespace HK.STG.Events
{
    public sealed class Move : UniRxEvent<Move, Vector3>
    {
        public Vector3 Velocity { get { return this.param1; } }
    }
}

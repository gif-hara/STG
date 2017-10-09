using HK.Framework.EventSystems;
using UnityEngine;

namespace HK.STG.Events
{
    public sealed class Hit : UniRxEvent<Hit, Collider2D>
    {
        public Collider2D Collider2D { get { return this.param1; } }
    }
}

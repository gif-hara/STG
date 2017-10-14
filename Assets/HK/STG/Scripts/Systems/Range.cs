using System;

namespace HK.STG.Systems
{
    [Serializable]
    public struct Range
    {
        public float Min;

        public float Max;

        public float Random
        {
            get { return UnityEngine.Random.Range(this.Min, this.Max); }
        }
    }
}

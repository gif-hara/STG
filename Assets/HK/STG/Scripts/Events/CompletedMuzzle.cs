using HK.Framework.EventSystems;
using HK.STG.DanmakuSystems;

namespace HK.STG.Events
{
    /// <summary>
    /// 1個単位の<see cref="Muzzle"/>が撃ち終わった際のイベント
    /// </summary>
    public sealed class CompletedMuzzle : UniRxEvent<CompletedMuzzle, Muzzle>
    {
        private static CompletedMuzzle cache = new CompletedMuzzle();

        public static CompletedMuzzle GetCache(Muzzle muzzle)
        {
            cache.param1 = muzzle;
            return cache;
        }
        
        public Muzzle Muzzle { get { return this.param1; } }
    }
}

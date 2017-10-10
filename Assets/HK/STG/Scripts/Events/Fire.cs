using HK.Framework.EventSystems;

namespace HK.STG.Events
{
    public sealed class Fire : UniRxEvent<Fire>
    {
        private static Fire cache = new Fire();

        public static Fire GetCache()
        {
            return cache;
        }
    }
}

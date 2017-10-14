using HK.Framework.EventSystems;
using HK.STG.CharacterControllers;

namespace HK.STG.Events
{
    /// <summary>
    /// プレイヤーが生成された際のイベント
    /// </summary>
    public sealed class PlayerSpawned : UniRxEvent<PlayerSpawned, Character>
    {
        public Character Player { get { return this.param1; } }
    }
}

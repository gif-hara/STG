using HK.STG.CharacterController;
using HK.STG.Events;
using HK.STG.GameSystems;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

namespace HK.STG.Extensions
{
    public static partial class Extensions
    {
        public static void SetupAsBullet(this Character self, Transform muzzle, float speed)
        {
            self.CachedTransform.position = muzzle.position;
            self.CachedTransform.rotation = muzzle.rotation;
            self.UpdateAsObservable()
                .TakeUntilDisable(self)
                .SubscribeWithState(self, (_, _character) =>
                {
                    _character.Broker.Publish(Move.Get(_character.CachedTransform.up * speed));
                });
            GameWorld.ReceiveScreenOut(self.Broker, self.CachedTransform, Vector2.zero)
                .TakeUntilDisable(self)
                .SubscribeWithState(self, (s, _self) => _self.PoolOrDestroy());
        }
    }
}

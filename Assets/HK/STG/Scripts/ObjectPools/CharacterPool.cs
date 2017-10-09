using System.Collections.Generic;
using HK.STG.CharacterController;
using UniRx.Toolkit;
using UnityEngine;

namespace HK.STG.ObjectPools
{
    public sealed class CharacterPool : ObjectPool<Character>
    {
        private readonly Character character;
        
        public CharacterPool(Character character)
        {
            this.character = character;
        }

        protected override Character CreateInstance()
        {
            var instance = Object.Instantiate(this.character);
            instance.Pool = this;
            return instance;
        }

        private static Dictionary<Character, CharacterPool> cachedPools = new Dictionary<Character, CharacterPool>();
        public static Character Rent(Character original)
        {
            CharacterPool pool;
            if (!cachedPools.TryGetValue(original, out pool))
            {
                pool = new CharacterPool(original);
                cachedPools.Add(original, pool);
            }

            return pool.Rent();
        }
    }
}

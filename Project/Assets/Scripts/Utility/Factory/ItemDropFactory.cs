using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    [CreateAssetMenu]
    public class ItemDropFactory : GameObjectFactory
    {
        [SerializeField]
        List<ItemDrop> prefab = null;

        public ItemDrop Get(int id)
        {
            ItemDrop instance = CreateInstance<ItemDrop>(prefab[0]);
            instance.OriginalFactory = this;
            return instance;
        }

        public void Reclaim(ItemDrop drop)
        {
            //Debug.Assert(drop.OriginalFactory == this, "Wrong factory reclaimed!");
            Destroy(drop.gameObject);
        }

    }
}
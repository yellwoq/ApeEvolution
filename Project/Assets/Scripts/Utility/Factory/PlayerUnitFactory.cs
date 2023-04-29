using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    [CreateAssetMenu]
    public class PlayerUnitFactory : GameObjectFactory
    {

        [SerializeField]
        List<PlayerUnit> prefab = null;

        public PlayerUnit Get(int id)
        {
            PlayerUnit instance = CreateInstance(prefab[id]);
            instance.OriginalFactory = this;
            return instance;
        }

        public void Reclaim(PlayerUnit unit)
        {
            //Debug.Assert(unit.OriginalFactory == this, "Wrong factory reclaimed!");
            Destroy(unit.gameObject);
        }

    }

}
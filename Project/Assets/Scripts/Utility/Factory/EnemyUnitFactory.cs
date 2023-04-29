using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApeEvolution
{
    [CreateAssetMenu]
    public class EnemyUnitFactory : GameObjectFactory
    {

        [SerializeField]
        List<EnemyUnit> prefab=null;

        public EnemyUnit Get(int id)
        {
            EnemyUnit instance = CreateGameObjectInstance(prefab[id]);
            instance.OriginalFactory = this;
            return instance;
        }

        public void Reclaim(EnemyUnit unit)
        {
            Debug.Assert(unit.OriginalFactory == this, "Wrong factory reclaimed!");
            Destroy(unit.gameObject);
        }

    }

}
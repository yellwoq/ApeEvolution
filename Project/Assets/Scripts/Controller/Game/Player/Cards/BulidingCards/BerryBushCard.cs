using System.Collections;
using System.Collections.Generic;
using ApeEvolution;
using UnityEngine;

namespace ApeEvolution
{
    public class BerryBushCard : CombineSaveCard
    {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Count;
            Count = Random.Range(4, 8);
        }
    }
}


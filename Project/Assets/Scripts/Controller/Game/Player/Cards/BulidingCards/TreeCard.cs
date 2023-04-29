using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class TreeCard : CombineSaveCard
    {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Count;
            Count = Random.Range(3, 5);
        }
    }
}


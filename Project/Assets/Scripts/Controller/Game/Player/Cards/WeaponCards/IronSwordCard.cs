using System.Collections;
using System.Collections.Generic;
using ApeEvolution;
using UnityEngine;

namespace ApeEvolution
{
    public class IronSwordCard : CombineSaveCard
    {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Count;
            Count = 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using ApeEvolution;
using UnityEngine;

namespace ApeEvolution
{
    public class FarmCard : CombineSaveCard
    {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Forever;
        }
    }
}


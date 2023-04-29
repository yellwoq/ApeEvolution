using System.Collections;
using System.Collections.Generic;
using ApeEvolution;
using UnityEngine;

namespace ApeEvolution
{
    public class LoggingPlantsCard : CombineSaveCard
    {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Forever;
        }
    }
}


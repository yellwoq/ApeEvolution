using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class HomeCard : CombineSaveCard
    {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Forever;
        }
    }
}



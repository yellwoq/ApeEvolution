using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-13
    //组合时保留卡片
    //**********
    public class CombineSaveCard : PlayerCard
    {
        public CombineSaveType MSaveType { get; protected set; }
        public int Count;
        protected virtual void Awake()
        {
            Count = Random.Range(3, 6);
        }
    }

    public enum CombineSaveType
    {
        Forever,
        Count
    }
}

using ApeEvolution;
using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;


public class anyCard:PlayerCard
{
    private void Awake()
    {
        this.SendCommand<AddFoodCommand>();
        Destroy(gameObject);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using ApeEvolution;

public class ConsumeFoodCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.GetModel<IPlayerDataModel>().foodStore.Value -= (MixedManager.GetPlayerUnits().Length) * 2;
        if (this.GetModel<IPlayerDataModel>().foodStore.Value <= 0)
        {
            this.SendEvent<GameLoseEvent>();
        }
    }
}

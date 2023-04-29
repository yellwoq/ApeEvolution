using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ApeEvolution
{
    public class AddFoodCommand : AbstractCommand
    {
        
        protected override void OnExecute()
        {
            this.SendCommand<AddExprienceCommand>(new AddExprienceCommand(10));
            this.GetModel<IPlayerDataModel>().foodStore.Value += 1;
        }
    }
}
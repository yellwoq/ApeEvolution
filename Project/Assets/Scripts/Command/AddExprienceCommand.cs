using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class AddExprienceCommand : AbstractCommand
    {
        int _value;

        public AddExprienceCommand(int value)
        {
            _value = value;
        }

        protected override void OnExecute()
        {
            var model =
            this.GetModel<IExperienceModel>();

            _value *= (int)this.GetModel<IPlayerDataModel>().expObtainRate;

            if (model.CurrentExprience.Value + _value >= model.LevelExprienceCost.Value)
            {
                model.CurrentExprience.Value += (_value - model.LevelExprienceCost.Value);
                this.SendCommand(new PlayVoiceCommand("DM-CGS-33", Vector3.zero));
                model.UpgradeLevel();
            }
            else
            {
                this.SendCommand(new PlayVoiceCommand("DM-CGS-45", Vector3.zero));
                model.CurrentExprience.Value += _value;
            }
        }



    }
}
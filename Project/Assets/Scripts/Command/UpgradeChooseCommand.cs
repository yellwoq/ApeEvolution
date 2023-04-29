using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class UpgradeChooseCommand:AbstractCommand
    {
        ChoiceCard _card;
        public UpgradeChooseCommand(ChoiceCard card)
        {
            _card = card;
        }

        protected override void OnExecute()
        {
            var model = this.GetModel<IPlayerDataModel>();
            model.ethnicHP *=_card.HPRate;
            model.ethnicATK *= _card.ATKRate;
            model.ethticATKSpd.Value *= _card.ATKSpdRate;
            model.synthesisTime.Value *= _card.SynSpd;
            model.matingTime.Value *= _card.MatingSpd;
        }
    }
}
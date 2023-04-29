using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ApeEvolution
{
    public class KillAllieCommand : AbstractCommand
    {
        private PlayerUnit _allie;

        public KillAllieCommand(PlayerUnit allie)
        {
            _allie = allie;
        }
        protected override void OnExecute()
        {
            _allie.OnDead.Invoke(_allie);
            this.SendCommand(new PlayVoiceCommand("DM-CGS-08", Vector3.zero));
            MixedManager.Instance.RecyclePlayerUnit(_allie);
        }
    }
}
using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ApeEvolution
{
    public class KillEnemyCommand : AbstractCommand
    {
        private EnemyUnit _enemy;

        public KillEnemyCommand(EnemyUnit enemy)
        {
            _enemy = enemy;
        }
        protected override void OnExecute()
        {
            
            _enemy.OnDead(_enemy);
            this.SendCommand(new PlayVoiceCommand("DM-CGS-26", Vector3.zero));
            this.SendCommand<AddExprienceCommand>(new AddExprienceCommand(this.GetModel<IPlayerDataModel>().expGainAmountInt.Value));
            MixedManager.Instance.RecycleEnemy(_enemy);
        }
    }
}
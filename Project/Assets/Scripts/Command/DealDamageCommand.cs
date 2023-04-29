using ApeEvolution;
using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class DealDamageCommand : AbstractCommand
    {
        bool _fromPlayer = false;
        int _instanceID = 0;
        int _damage = 1;

        public DealDamageCommand(int damage, bool fromPlayer, int id)
        {
            _damage = damage;
            _fromPlayer = fromPlayer;
            _instanceID = id;
        }

        protected override void OnExecute()
        {
            if (_fromPlayer)
            {
                var data = this.GetModel<IEnemyUnitModel>().GetUnitDataByInstanceID(_instanceID);
                //TODO 对敌方造成伤害
                data.HP.Value -= _damage;
                if (data.HP.Value <= 0)
                {
                    data.unit.Die();

                    //this.SendCommand<KillEnemyCommand>(new KillEnemyCommand(data.unit as EnemyUnit));
                }
                else
                {
                    data.unit.GetDamage();
                }
                this.SendCommand(new PlayVoiceCommand("DM-CGS-03", Vector3.zero));
                //Debug.Log("enemy" + this.GetModel<IEnemyUnitModel>().GetUnitDataByInstanceID(_instanceID).HP.Value);
            }
            else
            {
                var data = this.GetModel<IPlayerUnitModel>().GetUnitDataByInstanceID(_instanceID);
                data.HP.Value -= _damage;

                if (data.HP.Value <= 0)
                {
                    data.unit.Die();

                    //this.SendCommand<KillAllieCommand>(new KillAllieCommand(data.unit as PlayerUnit));
                }
                else
                {
                    data.unit.GetDamage();
                }
                //Debug.Log("player" + this.GetModel<IPlayerUnitModel>().GetUnitDataByInstanceID(_instanceID).HP.Value);
            }
        }
    }
}

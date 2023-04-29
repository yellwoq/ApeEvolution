/*
 * FileName:    EnterCombatCommand
 * Author:      清色
 * CreateTime:  #CREATETIME#
 * Description:
 * 
*/

using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class EnterCombatCommand : AbstractCommand
    {
        private EnemyUnit enemy;
        private PlayerUnit allie;

        public EnterCombatCommand(EnemyUnit enemy, PlayerUnit allie)
        {
            this.enemy = enemy;
            this.allie = allie;
        }

        protected override void OnExecute()
        {
            this.GetSystem<ICombatSystem>().CreateCombat(enemy,allie);
        }
    }

}
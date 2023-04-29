using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public class EnemyDropCommand : AbstractCommand
    {
        private Vector2 position;
        ItemDropData dropData;

        public EnemyDropCommand(Vector2 position, ItemDropData dropData)
        {
            this.position = position;
            this.dropData = dropData;
        }

        protected override void OnExecute()
        {

            //this.GetSystem<IGenerateSystem>().GenerateItemDrop(position,dropData);
        }
    }
}

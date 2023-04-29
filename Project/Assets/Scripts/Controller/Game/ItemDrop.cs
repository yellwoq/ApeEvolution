using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ApeEvolution
{

    public class ItemDrop : ApeEvolutionController
    {
        public ItemDropFactory OriginalFactory = null;

       
        private float timer = 0;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= this.GetModel<IPlayerDataModel>().synthesisTime)
            {
                this.SendCommand<AddFoodCommand>(new AddFoodCommand());
                timer -= this.GetModel<IPlayerDataModel>().synthesisTime;
            }
        }
    
    }

    public class ItemDropData
    {
        public int itemID;
        public int amount;
        public ItemDropData(int itemID, int amount)
        {
            this.itemID = itemID;
            this.amount = amount;
        }
    }
}
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-13
    //售出商品命令
    //**********
    public class SaleCardCommand : AbstractCommand
    {
        private CardContainer mContainer;

        public SaleCardCommand(CardContainer container)
        {
            this.mContainer = container;
        }
        protected override void OnExecute()
        {
            this.GetSystem<IStoreSystem>().SaleCards(mContainer);
        }

    }
}

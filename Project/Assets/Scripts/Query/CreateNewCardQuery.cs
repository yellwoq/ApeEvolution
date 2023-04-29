using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-12
    //ÐÂ½¨¿¨Æ¬²éÑ¯
    //**********
    public class CreateNewCardQuery : AbstractQuery<NewCardInfo>
    {
        ICanMoveItem[] playerCardList;
        public CreateNewCardQuery(params ICanMoveItem[] playerCardList)
        {
            this.playerCardList = playerCardList;
        }
        protected override NewCardInfo OnDo()
        {
            NewCardInfo currentInfo = this.GetSystem<IItemSynSystem>().GetComineResult(playerCardList);
            if (currentInfo!=null)
            {
                return currentInfo as NewCardInfo;
            }
            else
            {
                return null;
            }
        }
    }
}

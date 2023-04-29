using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //ø®∆¨∫œ≤¢÷∏¡Ó
    //**********
    public class PlayerCardMergeCommand : AbstractCommand
 {
        Vector2 targetPos;
        ICanMoveItem playerCard;
        public PlayerCardMergeCommand(Vector2 targetPos,ICanMoveItem playerCard)
        {
            this.targetPos = targetPos;
            this.playerCard = playerCard;
        }
        protected override void OnExecute()
        {
            PlayerCard pC = playerCard as PlayerCard;
            GameObject mTempContainer = pC.mContainer.gameObject;
            ICanMoveItem[] allPlayerCard = mTempContainer.GetComponentsInChildren<ICanMoveItem>();
            for (int i = 0; i < allPlayerCard.Length; i++)
            {
                (allPlayerCard[i] as PlayerCard).transform.SetParent(GameObject.Find("CardList").transform);
            }
            this.GetSystem<ICardSystem>().AddPlayerCards(targetPos, allPlayerCard.ToList());
            GameObject.Destroy(mTempContainer);
            this.SendCommand(new ItemComposeCommand(pC.mContainer.GetSortOrderAfterPlayerCards(0).ToArray()));
        }
 }
}

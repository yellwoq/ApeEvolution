using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //¿¨ÅÆÈÝÆ÷
    //**********
    public class CardContainer : MonoBehaviour
    {
        private List<ICanMoveItem> mPlayerCard;
        [HideInInspector]
        public Vector3 onMoveStartVector3;
        private void OnEnable()
        {
            onMoveStartVector3 = transform.position;
            mPlayerCard = new List<ICanMoveItem>();
        }
        public int GetCardCount()
        {
            return mPlayerCard.Count;
        }
        public void AddCard(ICanMoveItem playerCard)
        {
            playerCard.sortOrder = mPlayerCard.Count;
            PlayerCard addPlay = playerCard as PlayerCard;
            addPlay.transform.SetParent(transform);
            addPlay.mContainer = this;
            if (playerCard.sortOrder > 0)
            addPlay.transform.position = (mPlayerCard[0] as PlayerCard).transform.position
                - new Vector3(0, playerCard.sortOrder * 0.2f,0);
            else
            {
                addPlay.transform.localPosition = Vector3.zero;
            }
            mPlayerCard.Add(playerCard);
        }
        public List<ICanMoveItem> GetSortOrderAfterPlayerCards(int sortOrder)
        {
            return mPlayerCard.FindAll(e => e.sortOrder >= sortOrder);
        }
        public void RemoveCard(ICanMoveItem playerCard)
        {
            playerCard.mContainer = null;
            mPlayerCard.Remove(playerCard);
        }
    }
}

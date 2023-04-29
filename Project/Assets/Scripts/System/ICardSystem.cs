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
    //��Ƭϵͳ
    //**********
    public interface ICardSystem : ISystem
    {
        BindableProperty<Dictionary<Vector2,CardContainer>> PlayerCards { get; }
        void AddPlayerCard(Vector2 pos, ICanMoveItem addPlayerCard);
        void AddPlayerCards(Vector2 pos,List<ICanMoveItem> addPlayerCards);
        void RemoveCardAfterSortOrder(Vector2 pos, int sortOrder);
    }

    public class CardSystem : AbstractSystem, ICardSystem
    {
        public BindableProperty<Dictionary<Vector2, CardContainer>> PlayerCards { get; set; }
            = new BindableProperty<Dictionary<Vector2, CardContainer>>(new Dictionary<Vector2, CardContainer>());
        /// <summary>
        /// ���һ����Ϸ��Ƭ
        /// </summary>
        /// <param name="pos">��Ƭλ��</param>
        /// <param name="addPlayerCard">��Ƭ</param>
        public void AddPlayerCard(Vector2 pos, ICanMoveItem addPlayerCard)
        {
            if (PlayerCards.Value.ContainsKey(pos))
            {
                PlayerCards.Value[pos].AddCard(addPlayerCard);
            }
            else
            {
                GameObject container=GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/CardContainer"));
                container.name = pos.ToString();
                container.transform.position = pos;
                container.GetComponent<CardContainer>().AddCard(addPlayerCard);
                PlayerCards.Value.Add(pos, container.GetComponent<CardContainer>());
            }
        }

        public void AddPlayerCards(Vector2 pos, List<ICanMoveItem> addPlayerCards)
        {
            foreach (var card in addPlayerCards)
            {
                AddPlayerCard(pos, card);
            }
        }
        /// <summary>
        /// �Ƴ�ָ������������еĿ�Ƭ
        /// </summary>
        /// <param name="pos">λ��</param>
        /// <param name="sortOrder">ָ������</param>
        public void RemoveCardAfterSortOrder(Vector2 pos, int sortOrder)
        {
            if (PlayerCards.Value.ContainsKey(pos))
            {
                CardContainer container = PlayerCards.Value[pos];
                List<ICanMoveItem> allCards=container.GetSortOrderAfterPlayerCards(sortOrder);
                GameObject tempGo = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/CardContainer"));
                tempGo.name = "TEMP";
                CardContainer tempContainer = tempGo.GetComponent<CardContainer>();
                tempContainer.transform.position = pos;
                for (int i = 0; i < allCards.Count; i++)
                {
                    container.RemoveCard(allCards[i]);
                    (allCards[i] as PlayerCard).transform.SetParent(tempGo.transform);
                    tempContainer.AddCard(allCards[i]);
                    allCards[i].sortOrder = 1000 + i;
                }
                if (PlayerCards.Value[pos].GetCardCount() == 0)
                {
                    GameObject.Destroy(PlayerCards.Value[pos].gameObject);
                    PlayerCards.Value.Remove(pos);
                }
            }
        }

        protected override void OnInit()
        {
           
        }


    }
}

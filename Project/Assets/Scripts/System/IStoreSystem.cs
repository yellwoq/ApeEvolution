using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using QFramework;
using System;
using System.Linq;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-13
    //…ÃµÍœµÕ≥
    //**********
    public interface IStoreSystem : ISystem
    {
        BindableProperty<int> CurrentMoney { get; }
        BindableProperty<List<TradeGoodInfo>> TradeGoodInfoList { get; }
        void SaleCards(CardContainer saleCotainer);
        void BuyCardPackage(Type BuyPackageType);
    }

    public class TradeGoodInfo
    {
        public Type goodType;
        public int price;

        public TradeGoodInfo(Type goodType,int price)
        {
            this.goodType = goodType;
            this.price = price;
        }
    }
    public class MoneyUpdateEvent
    {
        public int moneyValue;
    }
    public class StoreSystem : AbstractSystem, IStoreSystem
    {
        public BindableProperty<int> CurrentMoney { get; set; } =new BindableProperty<int>(100);
        public BindableProperty<List<TradeGoodInfo>> TradeGoodInfoList { get; } = new BindableProperty<List<TradeGoodInfo>>();

        protected override void OnInit()
        {
            TradeGoodInfoList.Value = new List<TradeGoodInfo>()
            {
                new TradeGoodInfo(typeof(FoodCard),1),
                new TradeGoodInfo(typeof(FoodCardPackage),3),
                new TradeGoodInfo(typeof(MaterialCardPackage),3),
                new TradeGoodInfo(typeof(RandomCardPackage),5)
            };
        }
        public void SaleCards(CardContainer saleCotainer)
        {
            int addMoney = 0;
            PlayerCard[] currentAllCard = saleCotainer.GetComponentsInChildren<PlayerCard>();
            List<ICanMoveItem> returnBackCards = new List<ICanMoveItem>();
            for (int i = 0; i < currentAllCard.Length; i++)
            {
                TradeGoodInfo currentCardInfo = TradeGoodInfoList.Value.Find(t => t.goodType == currentAllCard[i].GetType());
                if (currentCardInfo != null)
                {
                    addMoney += currentCardInfo.price;
                    this.GetSystem<ICardSystem>().PlayerCards.Value[saleCotainer.transform.position].RemoveCard(currentAllCard[i]);
                }
                else
                {
                    returnBackCards.Add(currentAllCard[i]);
                }
            }
            Vector3 newPosition = GetCreatePosition();
            this.GetSystem<ICardSystem>().AddPlayerCards(newPosition, returnBackCards);
            GameObject.Destroy(saleCotainer.gameObject);
            CurrentMoney.Value+= addMoney;
        }
        private Vector3 GetCreatePosition()
        {
            Vector3 currentMouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 createPositon = new Vector3(currentMouseScreenPos.x, currentMouseScreenPos.y, 0)
                + new Vector3(Random.Range(-3f, 3f), Random.Range(-4f, 0f), 0);
            PlayerCard[] allExistsCard = GameObject.FindObjectsOfType<PlayerCard>();
            while (allExistsCard.Any(card => card.transform.position == createPositon))
            {
                currentMouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                createPositon = currentMouseScreenPos + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            }
            return createPositon;
        }
        public void BuyCardPackage(Type BuyPackageType)
        {

            Vector3 createPosition = GetCreatePosition();
            TradeGoodInfo currentCardInfo = TradeGoodInfoList.Value.Find(t => t.goodType == BuyPackageType);
            if (currentCardInfo!=null && CurrentMoney.Value>= currentCardInfo.price)
            {
                GameObject packageInstance = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/CardPackage"), createPosition,Quaternion.identity);
                packageInstance.AddComponent(BuyPackageType);
                packageInstance.name = nameof(BuyPackageType);
                CurrentMoney.Value-= currentCardInfo.price;
            }
        }
    }

}

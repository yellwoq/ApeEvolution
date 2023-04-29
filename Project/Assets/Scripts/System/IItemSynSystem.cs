using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QFramework;
using UnityEngine;

namespace ApeEvolution
{
    /// <summary>
    /// Author:HUANG
    /// Time：2023-2-12
    /// 物品合成系统
    /// </summary>
    public interface IItemSynSystem:ISystem
    {
        BindableProperty<float> SynTimeRatio { get; set; }
        BindableProperty<Dictionary<string, NewCardInfo>> ItemSynList { get;}
        NewCardInfo GetComineResult(params string[] cardList);
        NewCardInfo GetComineResult(params ICanMoveItem[] cardList);
    }
    public class NewCardInfo
    {
        public Type newCardType;
        public CurrentCulture mCulture;
        public float createTime;

        public NewCardInfo(Type cardType, CurrentCulture mCulture,float createTime)
        {
            newCardType = cardType;
            this.mCulture = mCulture;
            this.createTime = createTime;
        }
    }
    public class ItemSynSystem : AbstractSystem, IItemSynSystem
    {
        public BindableProperty<Dictionary<string, NewCardInfo>> ItemSynList { get; private set; }
            = new BindableProperty<Dictionary<string, NewCardInfo>>(
                new Dictionary<string, NewCardInfo>());
        public BindableProperty<float> SynTimeRatio { get; set; } = new BindableProperty<float>(1);


        protected override void OnInit()
        {
            float synTime=this.GetModel<IPlayerDataModel>().synthesisTime;

            ItemSynList.Value.Add("Role+Role", new NewCardInfo(typeof(BabyCard),CurrentCulture.IKariam,this.GetModel<IPlayerDataModel>().matingTime));
            //ItemSynList.Value.Add("Role+Food", new NewCardInfo(typeof(anyCard), CurrentCulture.IKariam, synTime));
            //ItemSynList.Value.Add("Food+Food", new NewCardInfo(typeof(RoleCard), CurrentCulture.IKariam,synTime));
            ItemSynList.Value.Add("Role+Tree", new NewCardInfo(typeof(WoodCard), CurrentCulture.IKariam,5));
            ItemSynList.Value.Add("Stone+Stone+Stone+Wood+Wood+Wood", new NewCardInfo(typeof(HomeCard), CurrentCulture.IKariam, 8));
            ItemSynList.Value.Add("BerryBush+Role", new NewCardInfo(typeof(BerryCard), CurrentCulture.IKariam, 5));
            ItemSynList.Value.Add("BananaTree+Role", new NewCardInfo(typeof(BananaCard), CurrentCulture.IKariam, 5));
            ItemSynList.Value.Add("Stone+Niter+Wood", new NewCardInfo(typeof(CampfireCard), CurrentCulture.StoneAge, 5));
            ItemSynList.Value.Add("Banana+Soil", new NewCardInfo(typeof(BananaTreeCard), CurrentCulture.StoneAge, 7));
            ItemSynList.Value.Add("Berry+Soil", new NewCardInfo(typeof(BerryBushCard), CurrentCulture.StoneAge, 5));
            ItemSynList.Value.Add("Banana+Campfire", new NewCardInfo(typeof(RoastBananaCard), CurrentCulture.StoneAge, 5));
            ItemSynList.Value.Add("Berry+Campfire", new NewCardInfo(typeof(RoastBerryCard), CurrentCulture.StoneAge, 5));
            ItemSynList.Value.Add("RawMeat+Campfire", new NewCardInfo(typeof(CookedMeatCard), CurrentCulture.StoneAge, 5));
            ItemSynList.Value.Add("Role+Rock", new NewCardInfo(typeof(NiterCard), CurrentCulture.StoneAge, 5));
            ItemSynList.Value.Add("Soil+Rock+Wood+Role", new NewCardInfo(typeof(FarmCard), CurrentCulture.Ancient, 5));
            ItemSynList.Value.Add("Wood+Wood+Wood+Stone+Role", new NewCardInfo(typeof(LoggingPlantsCard), CurrentCulture.Ancient, 10));
            ItemSynList.Value.Add("Wood+Stone+Stone+Stone+Role", new NewCardInfo(typeof(QuarryCard), CurrentCulture.Ancient, 10));
            ItemSynList.Value.Add("LoggingPlants+Role", new NewCardInfo(typeof(WoodCard), CurrentCulture.Ancient, 12));
            ItemSynList.Value.Add("Quarry+Role", new NewCardInfo(typeof(StoneCard), CurrentCulture.Ancient, 12));
            ItemSynList.Value.Add("Farm+Carrot", new NewCardInfo(typeof(CarrotCard), CurrentCulture.Ancient, 15));
            ItemSynList.Value.Add("Farm+Potato", new NewCardInfo(typeof(PotatoCard), CurrentCulture.Ancient, 15));
            ItemSynList.Value.Add("Campfire+Potato", new NewCardInfo(typeof(CookedPotatoCard), CurrentCulture.Ancient, 15));
            ItemSynList.Value.Add("Campfire+Carrot", new NewCardInfo(typeof(CookedCarrotCard), CurrentCulture.Ancient, 15));
        }

        public NewCardInfo GetComineResult(params string[] cardList)
        {
            string key = GetMapKey(cardList);
            if (ItemSynList.Value.ContainsKey(key))
            {
                return ItemSynList.Value[GetMapKey(cardList)];
            }
            else
            {
                return null;
            }
        }

        public NewCardInfo GetComineResult(params ICanMoveItem[] cardList)
        {
            string[] cardNames = new string[cardList.Length];
            for (int i = 0; i < cardList.Length; i++)
            {
                cardNames[i] = cardList[i].GetType().Name.Substring(0, cardList[i].GetType().Name.IndexOf("Card"));
            }
            return GetComineResult(cardNames);
        }

        private string GetMapKey(string[] cardNameList)
        {
            foreach (var combineKey in ItemSynList.Value.Keys)
            {
                if (CheckLogic(cardNameList, combineKey))
                {
                    return combineKey;
                }
            }
            return string.Empty;
        }

        private bool CheckLogic(string[] cardNameList,string e)
        {
            List<string> keyCardList = e.Split('+').ToList().FindAll(s => s != string.Empty);
            Dictionary<string, int> countList = new Dictionary<string, int>();
            for (int i = 0; i < cardNameList.Length; i++)
            {
                if (!keyCardList.Contains(cardNameList[i]))return false;
                if (!countList.ContainsKey(cardNameList[i]))
                {
                    countList.Add(cardNameList[i], 1);
                }
                else
                {
                    countList[cardNameList[i]]++;
                }
            }
            bool isPass = true;
            foreach (var keyCard in keyCardList)
            {
                if (countList.ContainsKey(keyCard))
                {
                    isPass &= countList[keyCard] == keyCardList.FindAll(s => s == keyCard).Count;
                }
                else
                {
                    isPass = false;
                    break;
                }
            }
            return isPass;
        }
    }
}

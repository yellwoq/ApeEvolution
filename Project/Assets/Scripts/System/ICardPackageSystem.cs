using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;

namespace ApeEvolution
{
    /// <summary>
    /// Author:HUANG
    /// Time:2023-2-12
    /// 卡包系统
    /// </summary>
    public interface ICardPackageSystem:ISystem
    {
        BindableProperty<Dictionary<string, List<CardInfo>>> CardPackageMap { get; }
        List<CardInfo> GetPackageInfos(CardPackageType packageType);
    }
    public enum CardPackageType
    {
        Food,
        Material,
        SpecialItem
    }
    public struct CardInfo
    {
        public Type cardType;
        public CurrentCulture belongerCulture;
        public int cardPercent;

        public CardInfo(Type cardType, CurrentCulture belongerCulture,int cardPercent)
        {
            this.cardType = cardType;
            this.belongerCulture = belongerCulture;
            this.cardPercent = cardPercent;
        }
    }

    public class CardPackageSystem : AbstractSystem, ICardPackageSystem
    {
        public BindableProperty<Dictionary<string, List<CardInfo>>> CardPackageMap { get; private set; }
        = new BindableProperty<Dictionary<string, List<CardInfo>>>(new Dictionary<string, List<CardInfo>>());
        public List<CardInfo> GetPackageInfos(CardPackageType packageType)
        {
            if (CardPackageMap.Value.ContainsKey(packageType.ToString()))
            {
                return CardPackageMap.Value[packageType.ToString()].FindAll(
                    culture=>culture.belongerCulture==this.GetSystem<ITimeSystem>().MCurrentCulture);
            }
            else return null;
        }

        private List<CardInfo> GetAllCardInfoByNames(string[] typeNameList,CurrentCulture[] belongerCultureList,int[] percentList)
        {
            if (typeNameList.Length != percentList.Length) return null;
            List<CardInfo> allCardInfo = new List<CardInfo>();
            for (int i = 0; i < typeNameList.Length; i++)
            {
                Type cardType = Type.GetType(this.GetType().Namespace + "." +string.Concat(typeNameList[i],"Card"));
                if (cardType != null)
                {
                    allCardInfo.Add(new CardInfo(cardType, belongerCultureList[i],percentList[i]));
                }
            }
            return allCardInfo;
        }
        protected override void OnInit()
        {
            string[] packageTypeNames = Enum.GetNames(typeof(CardPackageType));
            CardPackageMap.Value.Add(packageTypeNames[0], GetAllCardInfoByNames(new string[] { "Berry", "Banana" , "RoastBanana" , "RoastBerry","RawMeat","Carrot","RoastCarrots","RoastPotato" },
                new CurrentCulture[] { CurrentCulture.IKariam, CurrentCulture.IKariam,CurrentCulture.IKariam,CurrentCulture.IKariam,CurrentCulture.IKariam,CurrentCulture.IKariam,CurrentCulture.IKariam,CurrentCulture.IKariam }, new int[] { 50,30,20,30,20,10,10,10 }));
            CardPackageMap.Value.Add(packageTypeNames[1], GetAllCardInfoByNames(new string[] { "Wood", "Stone" }, new CurrentCulture[] { CurrentCulture.IKariam, CurrentCulture.IKariam }, new int[] { 50, 50 }));
            CardPackageMap.Value.Add(packageTypeNames[2], GetAllCardInfoByNames(new string[] { "BananaTree", "BerryBush","Rock" }, new CurrentCulture[] { CurrentCulture.IKariam, CurrentCulture.IKariam ,CurrentCulture.IKariam}, new int[] { 30, 50,60 }));
            // for (int i = 0; i < packageTypeNames.Length; i++)
            // {
            //     CardPackageMap.Value.Add(packageTypeNames[i], GetAllCardInfoByNames(new string[] { "Food", "Role" }, new CurrentCulture[] { CurrentCulture.IKariam, CurrentCulture.IKariam }, new int[] { 40, 60 }));
            // };
        }
    }

}

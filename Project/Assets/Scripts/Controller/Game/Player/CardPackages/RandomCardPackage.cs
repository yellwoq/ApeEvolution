using QFramework;
using System.Threading.Tasks;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-13
    //随机类卡牌包
    //**********
    public class RandomCardPackage:CardPackage
    {
        protected override void OnEnable()
        {
            MCardPakageType = CardPackageType.SpecialItem;
            MCardInfoList = this.SendQuery(new GetCurrentCardInfosQuery(CardPackageType.Food)).mCardInfo;
            MCardInfoList.AddRange(this.SendQuery(new GetCurrentCardInfosQuery(CardPackageType.Material)).mCardInfo);
            currentPackageNum = Random.Range(1, 3);
            mShowNumTxt.text = currentPackageNum.ToString();
        }
    }
}

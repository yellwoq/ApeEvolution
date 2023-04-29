using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
  //*************
  //Author:HUANG
  //Time:2023-2-13
  //食物类卡牌包
  //**********
  public class FoodCardPackage : CardPackage
 {
        protected override void OnEnable()
        {
            MCardPakageType = CardPackageType.Food;
            base.OnEnable();
        }
    }
}

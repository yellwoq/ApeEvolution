using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
  //*************
  //Author:HUANG
  //Time:2023-2-13
  //�����࿨�ư�
  //**********
  public class MaterialCardPackage :CardPackage
 {
        protected override void OnEnable()
        {
            MCardPakageType = CardPackageType.Food;
            base.OnEnable();
        }
    }
}

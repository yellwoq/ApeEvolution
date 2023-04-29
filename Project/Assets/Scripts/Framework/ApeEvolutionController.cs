using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
  //*************
  //Author:19-HUANG
  //Time:2023-2-11
  //表现层基类
  //**********
  public class ApeEvolutionController :MonoBehaviour,IController
 {
        EnemyUnit Enemy;
        IUnit Allie;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return ApeEvolutionArchitecture.Interface;
        }
 }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
  //*************
  //Author:HUANG
  //Time:2023-2-14
  //������
  //**********
  public class DontDestroyOnLoad : MonoBehaviour
 {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

        }
    }
}

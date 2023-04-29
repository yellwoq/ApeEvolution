using JetBrains.Annotations;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
  //*************
  //Author:HUANG
  //Time:2023-2-12
  //Ó¤¶ù¿¨Æ¬
  //**********
  public class BabyCard : CombineSaveCard
 {
        protected override void Awake()
        {
            MSaveType = CombineSaveType.Forever;
        }
        private void Start()
        {
            Invoke("GrowToAdult", 16);
        }

        private void GrowToAdult()
        {
            //var unit = MixedManager.Instance.GeneratePlayerUnit(transform.position);
            //unit.transform.parent=transform.parent;
            var unit = MixedManager.Instance.GeneratePlayerUnit(transform.position);
            this.GetSystem<ICardSystem>().AddPlayerCard(transform.position,unit.GetComponent<PlayerCard>());

            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}

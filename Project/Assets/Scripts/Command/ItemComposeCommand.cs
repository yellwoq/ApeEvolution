using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
  //*************
  //Author:HUANG
  //Time:2023-2-12
  //物体组合命令
  //**********
  public class ItemComposeCommand : AbstractCommand
 {
        private ICanMoveItem[] playerCardList;
        public ItemComposeCommand(params ICanMoveItem[] playerCardList)
        {
            this.playerCardList = playerCardList;
        }
        protected override void OnExecute()
        {
            CardContainer currenContainer = playerCardList[0].mContainer;
            CreateNewCardSlider currentSlider = currenContainer.GetComponentInChildren<CreateNewCardSlider>(true);
            currentSlider.MCardInfo = this.SendQuery(new CreateNewCardQuery(playerCardList));
            if (currentSlider.MCardInfo != null)
            {
                CurrentCulture mCulture = this.GetSystem<ITimeSystem>().MCurrentCulture;
                if ((int)currentSlider.MCardInfo.mCulture > (int)mCulture) return;
                currentSlider.gameObject.SetActive(true);
                currentSlider.CombineRatio = this.GetSystem<IItemSynSystem>().SynTimeRatio.Value;
                currentSlider.StartCoroutine(currentSlider.CreateNewCardHandle());
            }
        }
 }
}

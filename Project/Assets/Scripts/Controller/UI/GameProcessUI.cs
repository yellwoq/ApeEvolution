using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;
using System;
using TMPro;

namespace ApeEvolution
{
  //*************
  //Author:HUANG
  //Time:2023-2-14
  //��Ϸ��ʾ����
  //**********
  public class GameProcessUI : ApeEvolutionController
 {
        public TextMeshProUGUI showTimeTxt;
        public TextMeshProUGUI showDisasterTxt;
        public TextMeshProUGUI moneyShowTxt;
        void Start()
        {
            this.GetSystem<IStoreSystem>().CurrentMoney.RegisterWithInitValue(e =>
            {
                moneyShowTxt.text = $"��ң�{e}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<TimeUpdateEvent>(e=>
            {
                showTimeTxt.text = e.timeShowInfo;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<DisasterShowEvent>(e =>
            {
                showDisasterTxt.gameObject.SetActive(e.showState);
                showDisasterTxt.text =$"ͻ���¼�:{e.disasterName}";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public void OnBuyFoodClick()
        {
            this.SendCommand(new BuyCardCommand(typeof(FoodCardPackage)));
        }
        public void OnBuyMaterialClick()
        {
            this.SendCommand(new BuyCardCommand(typeof(MaterialCardPackage)));
        }
        public void OnBuyRandomClick()
        {
            this.SendCommand(new BuyCardCommand(typeof(RandomCardPackage)));
        }
    }
}

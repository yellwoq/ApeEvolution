using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ApeEvolution;
using UnityEngine.UI;
using TMPro;
using QFramework;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace ApeEvolution
{
    public class ChoiceControl : ApeEvolutionController, IPointerClickHandler
    {
        [SerializeField]
        Image cardImg;
        [SerializeField]
        Image cardBack;
        [SerializeField]
        TextMeshProUGUI cardDes;
        [SerializeField]
        TextMeshProUGUI cardName;

        ChoiceCard choiceCard=null;

        public IArchitecture GetArchitecture()
        {
            return ApeEvolutionArchitecture.Interface;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            transform.parent.parent.GetComponent<ChoiceUIControl>().Reset();
            transform.parent.parent.gameObject.SetActive(false);
            this.GetSystem<ICombatSystem>().ContinueCombat();
            this.SendCommand<UpgradeChooseCommand>(new UpgradeChooseCommand(choiceCard));
        }

        public void SetCard(Sprite sp, string name, ChoiceCard CC,Sprite icon)
        {
            choiceCard = CC;
            cardBack.sprite = sp;
            cardDes.text = CC.Description;
            cardImg.sprite = icon;
        }


    }
}
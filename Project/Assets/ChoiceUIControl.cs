using DG.Tweening;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ApeEvolution
{
    public class ChoiceUIControl : ApeEvolutionController,ICanRegisterEvent
    {

        [SerializeField]
        Sprite legendSprite;
        [SerializeField]
        Sprite epicSprite;
        [SerializeField]
        Sprite rareSprite;
        [SerializeField]
        RectTransform choiceText;

        [SerializeField]
        Sprite atkIcon;
        [SerializeField]
        Sprite atkSpdIcon;
        [SerializeField]
        Sprite HpIcon;
        [SerializeField]
        Sprite synSpd;
        [SerializeField]
        Sprite matingSpd;
        [SerializeField]
        Sprite ExpObtainRate;

        [SerializeField]
        List<ChoiceControl> choices = new List<ChoiceControl>();

        public void Reset()
        {
            this.DOComplete();
        }

        public void PopUpWindow()
        {
            this.GetSystem<ICombatSystem>().PauseCombat();
            choiceText.DOScale(1.1f, 0.6f).SetLoops(-1, LoopType.Yoyo);

            for (int i = 0; i < 3; i++)
            {
                var val = this.GetModel<IChoiceModel>().GetChoiceCard();
                Sprite sp = null;
                Sprite icon = null;
                switch (val.Elite)
                {
                    case CardElite.Legend:
                        sp = legendSprite;
                        break;
                    case CardElite.Epic:
                        sp = epicSprite;
                        break;
                    case CardElite.Rare:
                        sp = rareSprite;
                        break;
                }

                switch (val.icon)
                {
                    case IconType.Atk:
                        icon = atkIcon;
                        break;
                    case IconType.AtkSpd:
                        icon = atkSpdIcon;
                        break;
                    case IconType.MatingSpd:
                        icon = matingSpd;
                        break;
                    case IconType.SynSpd:
                        icon = synSpd;
                        break;
                    case IconType.Hp:
                        icon=HpIcon;
                        break;
                    case IconType.ExpObtainRate:
                        icon = ExpObtainRate;
                        break;
                }
                choices[i].transform.DOScale(1.1f, 0.6f).SetLoops(-1, LoopType.Yoyo);
                choices[i].SetCard(sp, "", val,icon);
            }
        }
    }
}
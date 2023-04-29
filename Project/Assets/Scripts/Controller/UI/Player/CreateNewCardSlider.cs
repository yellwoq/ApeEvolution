using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-12
    //合成新卡片显示条
    //**********
    public class CreateNewCardSlider : ApeEvolutionController
    {
        public NewCardInfo MCardInfo;
        private CardContainer MContainer;
        private Slider mSlider;
        private float ratio = 1;

        public float CombineRatio { get { return ratio; }set { value = ratio; } }
        private int currentCombineCount;
        private void Awake()
        {
            mSlider = GetComponent<Slider>();
            mSlider.maxValue = 100;
            MContainer = transform.GetComponentInParent<CardContainer>();

        }
        private void OnEnable()
        {
            mSlider.value = 0;
        }
        private void Update()
        {
            if (MContainer.GetCardCount() != currentCombineCount)
            {
                StopCoroutine(CreateNewCardHandle());
                gameObject.SetActive(false);
            }
        }
        private void OnDisable()
        {
            mSlider.value = 0;
        }
        public IEnumerator CreateNewCardHandle()
        {
            mSlider.gameObject.SetActive(true);
            Type newCardType = MCardInfo.newCardType;
            float createTime = MCardInfo.createTime;
            float passTimer = 0;
            currentCombineCount = MContainer.GetCardCount();
            yield return new WaitUntil(() =>
            {
                passTimer += Time.deltaTime*ratio;
                mSlider.value = 100 * passTimer / createTime;
                if (passTimer >= createTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
            mSlider.value = 100;
            CardContainer mContainer = transform.GetComponentInParent<CardContainer>();
            ICanMoveItem[] combineItems = mContainer.GetSortOrderAfterPlayerCards(0).ToArray();
            bool isCanContinue = true;
            ICanMoveItem[] allCreateSaveItems = Array.FindAll(combineItems, e => e is CombineSaveCard);
            int canCombineSaveCount = allCreateSaveItems.Length;
            isCanContinue &= canCombineSaveCount == combineItems.Length;
            for (int i = 0; i < combineItems.Length; i++)
            {
                if (combineItems[i].GetType().BaseType != typeof(CombineSaveCard))
                {
                    mContainer.RemoveCard(combineItems[i]);
                    Destroy((combineItems[i] as PlayerCard).transform.gameObject);
                }
                else
                {
                    if ((combineItems[i] as CombineSaveCard).MSaveType == CombineSaveType.Count)
                    {
                        (combineItems[i] as CombineSaveCard).Count--;
                        if((combineItems[i] as CombineSaveCard).Count <= 0)
                        {
                            isCanContinue &= false;
                            mContainer.RemoveCard(combineItems[i]);
                            Destroy((combineItems[i] as PlayerCard).transform.gameObject);
                        }
                    }
                }
            }
            CreatNewCard(mContainer, newCardType);
            if (isCanContinue)
            {
                yield return StartCoroutine(CreateNewCardHandle());
            }
            else
            {
                mSlider.gameObject.SetActive(false);
            }
          
        }

        private void CreatNewCard(CardContainer mContainer,Type newCardType)
        {
            //Vector3 createPositon = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            //PlayerCard[] allExistsCard = GameObject.FindObjectsOfType<PlayerCard>();
            //while (allExistsCard.Any(card => card.transform.position == createPositon))
            //{
            //    createPositon = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            //}
            GameObject newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
            Component addCard = newCard.AddComponent(newCardType);
            newCard.name = newCardType.Name;
            this.GetSystem<ICardSystem>().AddPlayerCard(mContainer.transform.position, newCard.GetComponent<PlayerCard>());
            newCard.transform.Find("CardNameText").GetComponent<TextMeshPro>().text = newCardType.Name;
            //StartCoroutine(CardMove(newCard, createPositon));
        }

        IEnumerator CardMove(GameObject targetCard, Vector3 targetPos)
        {
            yield return new WaitUntil(() =>
            {
                targetCard.transform.position = Vector3.MoveTowards(targetCard.transform.position, targetPos, 5 * Time.deltaTime);
                if (Vector3.Distance(targetCard.transform.position, targetPos) < 0.1f)
                {
                    return true;
                }
                return false;
            });
            this.GetSystem<ICardSystem>().AddPlayerCard(targetPos, targetCard.GetComponent<PlayerCard>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using QFramework;
using TMPro;
using System;
using System.Linq;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-13
    //¿¨°ü
    //**********
    public class CardPackage : ApeEvolutionController
    {
        public bool isMouseSelect { get; set; }

        public CardPackageType MCardPakageType;
        protected TextMeshPro mShowNumTxt;
        protected int currentPackageNum;

        protected List<CardInfo> MCardInfoList;

        private float waitCheckTime = 0.5f;
        private float waitCheckMoveDistance = 0.2f;

        private bool IsMoseMove;
        public bool isLockClick;
        private void Awake()
        {
            mShowNumTxt = GetComponentInChildren<TextMeshPro>();
        }
        protected virtual void OnEnable()
        {
            MCardInfoList = this.SendQuery(new GetCurrentCardInfosQuery(MCardPakageType)).mCardInfo;
            currentPackageNum = Random.Range(3, 6);
            mShowNumTxt.text = currentPackageNum.ToString();
        }
        public virtual void MouseDownHandle()
        {
            StartCoroutine(WaitTimeCheck());
        }
        IEnumerator WaitTimeCheck()
        {
            Vector3 orginPos = GetMousePos();
            yield return new WaitForSeconds(waitCheckTime);
            Vector3 currentPos = GetMousePos();
            if(Vector3.Distance(currentPos,orginPos)< waitCheckMoveDistance)
            {
                IsMoseMove = false;
                CreateSingleCard();
            }
            else
            {
                IsMoseMove = true;
            }
        }
        public void CreateSingleCard()
        {
            if (currentPackageNum > 0)
            {
                isLockClick = true;
                int createCardIndex = 0;
                int sumPercent = 0;
                List<int> percentRatioList = new List<int>();
                percentRatioList.Add(0);
                for (int i = 0; i < MCardInfoList.Count; i++)
                {
                    sumPercent += MCardInfoList[i].cardPercent;
                    percentRatioList.Add(sumPercent);
                }
                percentRatioList.Add(101);
                int percentArea=Random.Range(1, 101);
                for (int i = 1; i < percentRatioList.Count-1; i++)
                {
                    if(percentArea>= percentRatioList[i-1]&& percentArea<= percentRatioList[i + 1])
                    {
                        createCardIndex = i-1;
                    }
                }
                Vector3 createPositon = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
                PlayerCard[] allExistsCard = GameObject.FindObjectsOfType<PlayerCard>();
                while (allExistsCard.Any(card=>card.transform.position==createPositon))
                {
                    createPositon = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
                }
                Type newCardType = MCardInfoList[createCardIndex].cardType;
                GameObject newCard = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
                Component addCard = newCard.AddComponent(newCardType);
                newCard.name = newCardType.Name;
                newCard.transform.Find("CardNameText").GetComponent<TextMeshPro>().text = newCardType.Name;
                StartCoroutine(CardMove(newCard, createPositon));
            }
        }
        IEnumerator CardMove(GameObject targetCard,Vector3 targetPos)
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
            currentPackageNum--;
            if(currentPackageNum<=0) Destroy(gameObject);
            mShowNumTxt.text = currentPackageNum.ToString();
            isLockClick = false;
        }
        private Vector3 GetMousePos()
        {
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(inputPos.x, inputPos.y, transform.position.z);
        }
        public virtual void MouseMoveHandle()
        {
            if (IsMoseMove)
            {
                Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = GetMousePos();
            }
        }

        public virtual void MouseUpHandle()
        {
            isMouseSelect = false;
        }
        void Update()
        {
            if (isMouseSelect)
            {
                if (Input.GetMouseButton(0))
                {
                    MouseMoveHandle();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    MouseUpHandle();
                }
            }
        }

    }
}

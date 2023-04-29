using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Rendering;

namespace ApeEvolution
{
  //*************
  //Author:19-HUANG
  //Time:2023-2-11
  //Íæ¼Ò¿¨ÅÆ»ùÀà
  //**********
  public class PlayerCard : ApeEvolutionController,ICanMoveItem
 {
	    public bool isRole = false;
        public bool isMouseSelect { get; set; }
        public int sortOrder { get; set; }
        public CardContainer mContainer { get; set; }

        public bool isCanMerge;
        private Vector3 mergeCardPos;

        private HashSet<int> ss = new HashSet<int>();
        private bool isStartSale;
        private bool isInStore;
        public virtual void MouseDownHandle()
        {
            this.SendCommand(new PlayerCardMoveCommand(mContainer.transform.position, sortOrder));
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mContainer)
            {
                mContainer.onMoveStartVector3 = inputPos;
            }
        }

        public virtual void MouseMoveHandle()
        {
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mContainer)
            {
                mContainer.transform.position = new Vector3(inputPos.x, inputPos.y, transform.position.z);
            }
        }
        
        public virtual void MouseUpHandle()
        {
            if (isCanMerge)
            {
                this.SendCommand(new PlayerCardMergeCommand(mergeCardPos, this));
                Debug.Log("mergeCardPos");
                isCanMerge = false;
            }
            else
            {
                this.SendCommand(new PlayerCardMergeCommand(mContainer.transform.position, this));
            }
            isMouseSelect = false;
            if (isInStore)
            {
                this.SendCommand(new SaleCardCommand(mContainer));
                isInStore = false;
            }
        }

        public virtual void SetOrderLayer()
        {
            GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
            GetComponentInChildren<SortingGroup>().sortingOrder = sortOrder + 1;
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
            SetOrderLayer();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerCard>() && isMouseSelect)
            {
                mergeCardPos =collision.gameObject.GetComponent<PlayerCard>().mContainer.transform.position;
                isCanMerge = true;
            }
            if (collision.CompareTag("Store") && isMouseSelect)
            {
                isInStore = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerCard>() && isMouseSelect)
            {
                isCanMerge = false;
            }
            if (collision.CompareTag("Store")&&isMouseSelect)
            {
                isInStore = false;
            }
        }
    }
}

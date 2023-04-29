using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //ÕÊº“ ‰»Î¿‡
    //**********
    public class PlayerInput : ApeEvolutionController
    {
        private void Awake()
        {
            PlayerCard[] allTestCard= FindObjectsOfType<PlayerCard>();
            for (int i = 0; i <allTestCard.Length; i++)
            {
                this.GetSystem<ICardSystem>().AddPlayerCard(allTestCard[i].transform.position, allTestCard[i]);
            }
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject currentClickGO = GetCurrentClickGameObject();
                Component targetCompone;
                if (currentClickGO != null)
                {
                    if (currentClickGO.TryGetComponent(typeof(PlayerCard), out targetCompone))
                    {
                        ICanMoveItem moveItem = targetCompone as ICanMoveItem;
                        moveItem.isMouseSelect = true;
                        this.SendCommand(new PlayerCardMoveCommand((moveItem as PlayerCard).mContainer.transform.position, moveItem.sortOrder));
                    }
                    else if (currentClickGO.TryGetComponent(typeof(CardPackage), out targetCompone))
                    {
                        CardPackage cardPack = targetCompone as CardPackage; 
                        cardPack.isMouseSelect = true;
                        if(!cardPack.isLockClick)
                            cardPack.MouseDownHandle();
                    }
                }
            }
        }

        GameObject GetCurrentClickGameObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction,20);
            if (hit.collider != null)
            {
                return hit.collider.gameObject;
            }
            else
            {
                return null;
            }
        }
    }
}

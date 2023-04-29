using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //��ʼ�ƶ���Ƭָ��
    //**********
    public class PlayerCardMoveCommand : AbstractCommand
    {
        Vector2 movePos;
        int moveCardOrder;
        public PlayerCardMoveCommand(Vector2 movePos, int moveCardOrder)
        {
            this.movePos= movePos;
            this.moveCardOrder = moveCardOrder;
        }
        protected override void OnExecute()
        {
            this.GetSystem<ICardSystem>().RemoveCardAfterSortOrder(movePos, moveCardOrder);
        }

    }
}

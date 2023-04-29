using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //�����ƶ��Ŀ�Ƭ�ӿ�
    //**********
    public interface ICanMoveItem
    {
        bool isMouseSelect { get; set; }
        int sortOrder { get; set; }
        CardContainer mContainer { get; set; }
        void MouseDownHandle();
        void MouseMoveHandle();
        void MouseUpHandle();
    }
}

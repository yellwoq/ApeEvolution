using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //Ö÷½Ç¿¨ÅÆ
    //**********
    public class RoleCard : CombineSaveCard
    {
        [SerializeField]List<SpriteRenderer> renderers;
        [SerializeField]List<SortingGroup> sortings;

        [SerializeField]
        SpriteRenderer Effect;

        protected override void Awake()
        {
            MSaveType=CombineSaveType.Forever;
            base.Awake();
        }
        public override void SetOrderLayer()
        {
            if (isRole)
            {
                foreach (var renderer in renderers)
                {
                    renderer.sortingOrder = sortOrder;
                }
                foreach (var renderer in sortings)
                {
                    renderer.sortingOrder = sortOrder + 1;
                }
                if (Effect != null)
                {
                    Effect.sortingOrder = sortOrder + 1;
                }
            }
            else
            {
                base.SetOrderLayer();
            }
        }
    }
}

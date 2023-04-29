using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-12
    //随机灾难数据模块
    //**********
    public interface IDisasterModel : IModel
    {
        BindableProperty<DisaterAffectList> CurrentAffectList { get; set; }
    }
    public class DisaterAffectList
    {
        public List<DisasterEffectInfo> allAffectInfo;

        public DisasterEffectInfo this[int index]
        {
            get { return allAffectInfo[index]; }
            set { allAffectInfo[index] = value; }
        }
    }

    public class DisasterModel : AbstractModel, IDisasterModel
    {
        public BindableProperty<DisaterAffectList> CurrentAffectList { get; set; }
            = new BindableProperty<DisaterAffectList>(new DisaterAffectList());
          

        protected override void OnInit()
        {
            
        }
    }
}

using QFramework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-12
    //ÔÖÄÑÏµÍ³
    //**********
    public interface IDisasterSystem : ISystem
    {
        BindableProperty<int> HappenPrecent { get; set; }
        BindableProperty<Dictionary<string, List<DisasterEffectInfo>>> DisasterInfoMap { get; }

        void HappenDisaster(DisasterType disasterType);
        void CancelDisaster(DisasterType disasterType);
    }
    public class DisasterShowEvent
    {
        public bool showState;
        public string disasterName;
    }
    public class DisasterEffectInfo
    {
        public Type AffectType;
        public string AffectFieldOrPropertyName;
        public AffectOperator currentOperator;
        public object affectValue;
        public DisasterEffectInfo(Type AffectType, string AffectFieldOrPropertyName,
            AffectOperator currentOperator, object affectValue)
        {
            this.AffectType = AffectType;
            this.AffectFieldOrPropertyName = AffectFieldOrPropertyName;
            this.affectValue = affectValue;
            this.currentOperator = currentOperator;
        }
    }
    public enum AffectOperator
    {
        Add,
        Sub,
        Create
    }
    public enum DisasterType
    {
        Storm,
        Tornadoes,
        MonsterInvasion
    }

    public class DisasterSystem : AbstractSystem, IDisasterSystem
    {
        protected override void OnInit()
        {
            DisasterInfoMap.Value.Add(DisasterType.Storm.ToString(), new List<DisasterEffectInfo>()
            {
                new DisasterEffectInfo(typeof(IItemSynSystem),"SynTimeRatio",AffectOperator.Add,0.5f)
            });
            DisasterInfoMap.Value.Add(DisasterType.Tornadoes.ToString(), new List<DisasterEffectInfo>()
            {

            });
            DisasterInfoMap.Value.Add(DisasterType.MonsterInvasion.ToString(), new List<DisasterEffectInfo>()
            {

            });
        }
        public BindableProperty<int> HappenPrecent { get; set; } = new BindableProperty<int>(50);

        public BindableProperty<Dictionary<string, List<DisasterEffectInfo>>> DisasterInfoMap { get; private set; } =
            new BindableProperty<Dictionary<string, List<DisasterEffectInfo>>>(new Dictionary<string, List<DisasterEffectInfo>>());

        public void CancelDisaster(DisasterType disasterType)
        {
            this.GetModel<IDisasterModel>().CurrentAffectList.Value.allAffectInfo = null;
            List<DisasterEffectInfo> rebackAffectList = DisasterInfoMap.Value[disasterType.ToString()];
            DisasterAffectHandle(rebackAffectList, false);
        }

        public void HappenDisaster(DisasterType disasterType)
        {
            List<DisasterEffectInfo> affectList = DisasterInfoMap.Value[disasterType.ToString()];
            this.GetModel<IDisasterModel>().CurrentAffectList.Value.allAffectInfo = affectList;
            DisasterAffectHandle(affectList);
        }

        private void DisasterAffectHandle(List<DisasterEffectInfo> affectList, bool isHappen = true)
        {
            for (int i = 0; i < affectList.Count; i++)
            {
                DisasterEffectInfo currentEffectInfo = affectList[i];
                Type affectType = currentEffectInfo.AffectType;

                Type architectureBaseType = typeof(ApeEvolutionArchitecture).BaseType;
                FieldInfo containerField = architectureBaseType.GetField("mContainer", BindingFlags.NonPublic | BindingFlags.Instance);
                IOCContainer iocContainer = containerField.GetValue(ApeEvolutionArchitecture.Interface) as IOCContainer;
                FieldInfo containerCacheField = typeof(IOCContainer).GetField("mInstances", BindingFlags.NonPublic | BindingFlags.Instance);
                Dictionary<Type, object> containerCache = containerCacheField.GetValue(iocContainer) as Dictionary<Type, object>;
                if (containerCache.ContainsKey(affectType))
                {
                    object resultInstance = containerCache[affectType];
                    PropertyInfo tryGetPropertyInfo = resultInstance.GetType().GetProperty(currentEffectInfo.AffectFieldOrPropertyName,
                    BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Public|BindingFlags.NonPublic| BindingFlags.Static |
                    BindingFlags.Instance);
                    FieldInfo tryGetFieldInfo = resultInstance.GetType().GetField(currentEffectInfo.AffectFieldOrPropertyName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField|BindingFlags.Static|
                    BindingFlags.Instance);
                    object affectBindData = tryGetPropertyInfo != null ? tryGetPropertyInfo.GetValue(resultInstance) : tryGetFieldInfo.GetValue(resultInstance);
                    //PropertyInfo affectBindDataProperty = affectBindData.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                    switch (currentEffectInfo.currentOperator)
                    {
                        case AffectOperator.Add:
                        case AffectOperator.Sub:
                            BindableProperty<float> currentAffectBP = affectBindData as BindableProperty<float>;
                            float orginValue = currentAffectBP.Value;
                            float changeValue = float.Parse(currentEffectInfo.affectValue.ToString());
                            if (currentEffectInfo.currentOperator== AffectOperator.Add)
                            {
                               
                            }
                            else
                            {
                                changeValue = -changeValue;
                            }
                            currentAffectBP.Value= orginValue + (isHappen ? changeValue : -changeValue);
                            break;
                        case AffectOperator.Create:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

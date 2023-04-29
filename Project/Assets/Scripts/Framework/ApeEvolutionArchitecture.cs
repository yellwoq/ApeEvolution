using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace ApeEvolution
{
    //*************
    //Author:19-HUANG
    //Time:2023-2-11
    //项目基础架构
    //**********
    public class ApeEvolutionArchitecture : Architecture<ApeEvolutionArchitecture>
    {
        protected override void Init()
        {
            this.RegisterSystem<IItemSynSystem>(new ItemSynSystem());
            this.RegisterSystem<ICardSystem>(new CardSystem());
            this.RegisterSystem<ICardPackageSystem>(new CardPackageSystem());
            this.RegisterSystem<ITimeSystem>(new TimeSystem());
            this.RegisterSystem<IDisasterSystem>(new DisasterSystem());
            this.RegisterSystem<IStoreSystem>(new StoreSystem());
            RegisterSystem<IGenerateSystem>(new GenerateSystem());
            RegisterSystem<ICombatSystem>(new CombatSystem());
            
            RegisterModel<IChoiceModel>(new ChoiceModel());
            RegisterModel<IPlayerDataModel>(new PlayerDataModel());
            this.RegisterModel<IDisasterModel>(new DisasterModel());
            RegisterModel<IExperienceModel>(new ExperienceModel());
            RegisterModel<IPlayerUnitModel>(new PlayerUnitModel());
            RegisterModel<IEnemyUnitModel>(new EnemyUnitModel());
            RegisterModel<IEnemyWaveModel>(new EnemyWaveModel());

        }
    }
}

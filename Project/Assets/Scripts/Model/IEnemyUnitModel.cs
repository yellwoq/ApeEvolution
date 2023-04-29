using ApeEvolution;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public interface IEnemyUnitModel : IModel
    {
        UnitData GetUnitDataByInstanceID(int id);

        bool RegisterUnitData(int id, UnitData unitData);
        bool UnregisterUnitData(int id);
    }

    public class EnemyUnitModel : AbstractModel, IEnemyUnitModel
    {
        private Dictionary<int, UnitData> unitDataDic = new Dictionary<int, UnitData>();

        public UnitData GetUnitDataByInstanceID(int id)
        {
            UnitData data;

            unitDataDic.TryGetValue(id, out data);

            return data;
        }

        public bool RegisterUnitData(int id, UnitData unitData)
        {
            unitDataDic.Add(id, unitData);

            //TODO 注册后要发生的东西

            return true;
        }

        public bool UnregisterUnitData(int id)
        {
            unitDataDic.Remove(id);

            return true;
        }

        protected override void OnInit()
        {
        }
    }

    public class EnemyUnitData : UnitData
    {
        public EnemyUnitData(int id, IUnit u, int hp, int atk,float atkS) : base(id, u, hp, atk,atkS)
        {
            //_instanceID = id;
            //_unit = u;
            //_atk = atk;
        }

        public override int InstanceID { get => _instanceID; }
        public override IUnit unit { get { return _unit; } set { _unit = value; } }

        public override BindableProperty<int> HP { get => _hp; } 

        public override int ATK { get => _atk; set { _atk = ATK; } }

        public override float ATKSpeed { get => _atkSpeed; set { _atkSpeed = value; } }

        public override void ResetData()
        {
            _instanceID = 0;
            _unit = null;
            _atk = 0;
        }
    }
}
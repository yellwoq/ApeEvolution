using JetBrains.Annotations;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

namespace ApeEvolution
{
    public interface IPlayerUnitModel : IModel
    {
        UnitData GetUnitDataByInstanceID(int id);

        bool RegisterUnitData(int id, UnitData unitData);
        bool UnregisterUnitData(int id);
    }

    public class PlayerUnitModel : AbstractModel, IPlayerUnitModel
    {
        private Dictionary<int, UnitData> unitDataDic = new Dictionary<int, UnitData>();

        public UnitData GetUnitDataByInstanceID(int id)
        {
            UnitData data = null;

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

    
    public class PlayerUnitData : UnitData
    {
        public PlayerUnitData(int id, IUnit u, int hp, int atk,float atkS) : base(id, u, hp, atk,atkS)
        {

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

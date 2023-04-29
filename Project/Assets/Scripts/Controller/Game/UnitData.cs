using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    [System.Serializable]
    public abstract class UnitData
    {
        protected int _instanceID;
        protected IUnit _unit;
        protected int _atk;
        protected BindableProperty<int> _hp;

        protected float _atkSpeed;
        protected float _missRate;


        protected int _atkHeredity = 2;
        protected int _hpHeredity = 4;


        public UnitData(int id, IUnit u, int hp, int atk,float atkS)
        {
            _instanceID = id;
            _unit = u;
            _atk = atk;
            _hp = new BindableProperty<int>(hp);
            _atkSpeed = atkS;
        }
        public float timer = 0;
        public abstract int InstanceID { get; }
        public abstract IUnit unit { get; set; }
        public abstract BindableProperty<int> HP { get; }
        public abstract int ATK { get; set; }
        public abstract float ATKSpeed { get; set; }

        public abstract void ResetData();

    }
}
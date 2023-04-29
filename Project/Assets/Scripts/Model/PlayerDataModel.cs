using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ApeEvolution
{

    public interface IPlayerDataModel : IModel
    {
        //public PlayerUnitData GetCurrentPlayerUnitData();
        public float ethnicHP { get; set; }
        public float ethnicATK { get; set; }
        public float expGainAmount { get; set; }
        public BindableProperty<int> expGainAmountInt { get; }
        public BindableProperty<int> ethticHPInt { get; }
        public BindableProperty<int> ethticATKInt { get; }
        public BindableProperty<float> ethticATKSpd { get; }
        public BindableProperty<float> expObtainProbability { get; }
        public BindableProperty<float> expObtainRate { get; }
        public BindableProperty<float> matingTime { get; }
        public BindableProperty<float> synthesisTime { get; }
        public BindableProperty<int> enemyDropLevel { get; }
        public BindableProperty<int> foodStore { get; }
        public BindableProperty<float> enemyDropProb { get; }
    }                                                        
    public class PlayerDataModel : AbstractModel, IPlayerDataModel
    {
        private float _ethnicHP = 20f;
        private float _ethnicATK = 5f;
        //private float _ethnicATKSpd = 1.5f;
        private float _expGain = 35;

        public float ethnicHP {
            get
            {
                return _ethnicHP;
            }
            set
            {
                _ethnicHP = value;
                ethticHPInt.Value = Mathf.FloorToInt(_ethnicHP);
                Debug.Log("ethHP:" + ethticHPInt.Value);
            }
        }
        public float ethnicATK { get { return _ethnicATK; } set {_ethnicATK=value; ethticATKInt.Value = Mathf.FloorToInt(_ethnicATK);
                Debug.Log("ethATK:" + ethticATKInt.Value);
            } }
        public float expGainAmount { get { return _expGain; } set {
                _expGain = value; expGainAmountInt.Value = Mathf.FloorToInt(_expGain);
                Debug.Log("expGain:" + expGainAmountInt.Value);
            } }

        public BindableProperty<int> ethticHPInt { get; } = new BindableProperty<int>(20);

        public BindableProperty<int> ethticATKInt { get; } = new BindableProperty<int>(5);

        public BindableProperty<int> expGainAmountInt { get; } = new BindableProperty<int>(35);

        public BindableProperty<float> expObtainProbability { get; } = new BindableProperty<float>(0.3f);

        public BindableProperty<float> expObtainRate { get; } = new BindableProperty<float>(1.0f);

        public BindableProperty<float> matingTime { get; } = new BindableProperty<float>(8);

        public BindableProperty<float> synthesisTime { get; } = new BindableProperty<float>(10);

        public BindableProperty<float> ethticATKSpd { get; } = new BindableProperty<float>(1.5f);

        public BindableProperty<int> enemyDropLevel { get; } = new BindableProperty<int>(0);

        public BindableProperty<float> enemyDropProb { get; } = new BindableProperty<float>(0.3f);

        public BindableProperty<int> foodStore { get; } = new BindableProperty<int>(10);

        protected override void OnInit()
        {

        }
    }




}
using DG.Tweening;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ApeEvolution
{
    public class PlayerUnit : MonoBehaviour, IController, IUnit
    {
        private bool _interactable = true;
        private UnitData _combatData;
        private CombatStatus _combatStatus;
        private Action<IUnit> _onDead;
        private int _instanceID;
        [SerializeField]Animator _animator;

        [SerializeField] TextMeshPro _hpText;
        [SerializeField] TextMeshPro _nameText;
        [SerializeField] TextMeshPro _atkText;
        //-----------------------UI

        private GameObjectFactory _originalFactory;

        private void Awake()
        {
            _instanceID=gameObject.GetInstanceID();

            if (_combatData == null)
            {
                _combatData = new PlayerUnitData(_instanceID, this, 20, 4, 1f);

            };

            this.GetModel<IPlayerUnitModel>().RegisterUnitData(_combatData.InstanceID, CombatData);

            CombatData.HP.Register(OnValueChanged).UnRegisterWhenGameObjectDestroyed(gameObject);

            //如果Model中的字典中的数据有变化，将反映到卡牌ui上
            this.GetModel<IPlayerUnitModel>().GetUnitDataByInstanceID(_combatData.InstanceID).HP.Register((e) =>
            {
                _hpText.text = e.ToString();
            });
            _atkText.text = _combatData.ATK.ToString();
            _hpText.text = _combatData.HP.ToString();

        }

        public void UpdateVisual()
        {
            _atkText.text = _combatData.ATK.ToString();
            _hpText.text = _combatData.HP.ToString();
        }

        private void OnValueChanged(int value)
        {
            _hpText.text = value.ToString();
        }

        public bool interactable
        {
            get { return _interactable; }
            set { _interactable = value; }
        }

        public UnitData CombatData
        {
            get { return _combatData; }
            set { _combatData = value; }
        }

        public CombatStatus CombatStatus
        {
            get { return _combatStatus; }
            set { _combatStatus = value; }
        }

        public int InstanceID => _instanceID;

        public Action<IUnit> OnDead { get =>_onDead; set { _onDead = value; } }

        public GameObjectFactory OriginalFactory { get => _originalFactory; set { _originalFactory = value; } }

        public IArchitecture GetArchitecture()
        {
            return ApeEvolutionArchitecture.Interface;
        }

        public void UpdateAction()
        {

        }

        public void TranslatePosition(Vector2 position)
        {
            transform.position = position;
        }

        public void PunchAnimate(Vector2 pos)
        {
            Vector2 tarPos = (Vector2)transform.position + (pos - (Vector2)transform.position) * 0.7f;

            transform.DOMove(tarPos, 0.1f).SetLoops(2, LoopType.Yoyo);
            //transform.DOShakePosition(0.3f, 0.5f, 0, 90, false, true);
        }

        public void Die()
        {
            _animator.SetTrigger("Burn");
        }

        public void GetDamage()
        {
            _animator.SetTrigger("Damage");
        }
    }
}
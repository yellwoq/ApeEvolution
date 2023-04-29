using DG.Tweening;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using System;
using UnityEngine;



namespace ApeEvolution
{

    public interface IUnit
    {
        GameObjectFactory OriginalFactory { get; set; }
        UnitData CombatData { get; set; }
        CombatStatus CombatStatus { get; set; }
        bool interactable { get; set; }
        void UpdateAction();
        void TranslatePosition(Vector2 position);

        void PunchAnimate(Vector2 pos);

        void Die();

        void GetDamage();

        int InstanceID { get; }

        Action<IUnit> OnDead { get; set; }
    }

    

    public class EnemyUnit : MonoBehaviour,IController,IUnit
    {
        [SerializeField] Transform target;

        Vector2 offset;
        //↑测试----------------------



        [SerializeField]TextMeshPro _hpText;
        [SerializeField]TextMeshPro _nameText;
        [SerializeField] TextMeshPro _atkText;

        [SerializeField] Animator _animator;

        //-----------------------UI
        [SerializeField] private float atkSpd = 2f;
        [SerializeField] private int atk = 2;
        [SerializeField] private int hp = 10;


        //-----------------------数值

        [SerializeField] private float _detectRange = 3f;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _moveDelay = 1f;
        [SerializeField]private bool _canMove=true;
        [SerializeField] private GameObjectFactory _originalFactory;


        public bool _interactable = true;
        private UnitData _combatData;
        CombatStatus _combatStatus = null;
        private Action<IUnit> _onDead;
        private int _instanceID;


        private float _moveTimer = 0f;

        public GameObjectFactory OriginalFactory { get => _originalFactory;set { _originalFactory = value; } }

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

        public Action<IUnit> OnDead {get { return _onDead;} set { _onDead = value; } }

        public int InstanceID => _instanceID;

        private void Awake()
        {
            _instanceID = gameObject.GetInstanceID();
            _combatData = new PlayerUnitData(_instanceID, this, hp, atk,atkSpd);
            IEnemyUnitModel _enemyUnitModel = this.GetModel<IEnemyUnitModel>();
            CombatData.HP.Register(OnValueChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
            _enemyUnitModel.RegisterUnitData(_combatData.InstanceID, CombatData);
            _enemyUnitModel.GetUnitDataByInstanceID(_combatData.InstanceID).HP.Register((e) =>
            {
                _hpText.text = e.ToString();
            });
            _atkText.text=atk.ToString();
            _hpText.text = hp.ToString();


            
        }

        private void OnValueChanged(int value)
        {
            _hpText.text=value.ToString();
        }

        private void Update()
        {
            _moveTimer += Time.deltaTime;

            UpdateAction();
        }


        /// <summary>
        /// 更新敌人行动，可由集合进行集体更新
        /// </summary>
        public void UpdateAction()
        {
            //优化方向 在玩家拖动位置后进行距离检测

            //Debug.Log(GetShortestDistanceToPlayer());

            if (interactable)
            {
                target = GetNearestAllie();
                if (target != null)
                {
                    if (Vector2.Distance(transform.position, target.position) < _detectRange)
                    {
                        if (_combatStatus == null)
                        {
                            EnterBattle(target.GetComponent<PlayerUnit>());
                        }
                    }
                    else
                    {
                        if (!_canMove) return;
                        if (_moveTimer > _moveDelay)
                        {
                            _moveTimer -= _moveDelay;
                            TrackPlayer(target.position);
                        }

                    }

                }
            }
            else
            {
                GameObject[] gos = MixedManager.GetPlayerUnits();

                foreach (GameObject unit in gos)
                {
                    float distance = Vector2.Distance(transform.position, unit.transform.position);

                    if (distance < _detectRange)
                    {
                        EnterBattle(unit.GetComponent<PlayerUnit>());
                    }
                }
            }

        }

        /// <summary>
        /// 获取最近的玩家单位 的
        /// transform
        /// </summary>
        /// <returns></returns>
        protected Transform GetNearestAllie()
        {
            //TODO 接入player 单位集合
            //Transform[] playerUnits = new Transform[5];
            float shortestDistance=float.MaxValue;

            Transform targetUnit=null;
            GameObject[] gos = MixedManager.GetPlayerUnits();
            foreach (GameObject unit in gos)
            {
                float distance = Vector2.Distance(transform.position, unit.transform.position);

                if(distance < shortestDistance)
                {
                    targetUnit = unit.transform;
                    shortestDistance = distance;
                }
            }

            return targetUnit;
        }


        /// <summary>
        /// 进入战斗
        /// </summary>
        protected void EnterBattle(PlayerUnit unit)
        {
            this.SendCommand<EnterCombatCommand>(new EnterCombatCommand(this,unit));
            //Debug.Log("进入战斗");
        }

        protected void TrackPlayer(Vector2 targetPos)
        {
            offset = targetPos - (Vector2)transform.position;
            offset = offset.normalized * _moveSpeed;
            //TODO
            Vector2 tp = (Vector2)transform.position + offset;
            transform.DOJump(tp, 0.1f, 1, 0.3f);
            //transform.Translate(offset);
        }

        public IArchitecture GetArchitecture()
        {
            return ApeEvolutionArchitecture.Interface;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position,transform.position + (Vector3)offset * 10);
        }

        public void TranslatePosition(Vector2 position)
        {
            transform.DOKill();

            transform.DOMove(position, 0.1f);
            
            //transform.position = position;
        }

        public void PunchAnimate(Vector2 pos)
        {
            Vector2 tarPos = (Vector2)transform.position + (pos - (Vector2)transform.position) * 0.7f;

            transform.DOMove(tarPos,0.1f).SetLoops(2,LoopType.Yoyo);
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


using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public interface ICombatSystem : ISystem
    {
        void CreateCombat(EnemyUnit enemy, PlayerUnit allie);
        void UpdateCombat();

        void PauseCombat();
        void ContinueCombat();
    }

    public class CombatSystem : AbstractSystem, ICombatSystem,ICanSendCommand
    {
        private List<CombatStatus> combats;
        private float combatSpeed = 1.0f;
        

        //TODO 需要修改 和替换 Allie
        public Dictionary<int,EnemyUnit> combatingEnemies;
        public Dictionary<int,PlayerUnit> combatingAllies;


        protected override void OnInit()
        {
            combatingAllies = new Dictionary<int,PlayerUnit>();
            combatingEnemies = new Dictionary<int,EnemyUnit>();
            combats = new List<CombatStatus>();
        }


        /// <summary>
        /// 创建战斗
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="allie"></param>
        public void CreateCombat(EnemyUnit enemy,PlayerUnit allie)
        {
            if(enemy.CombatStatus != null && allie.CombatStatus != null)
            {
                //Debug.LogError("无法创建战斗");
                return;
            }
            else if (enemy.CombatStatus == null && allie.CombatStatus == null)
            {
                //待修改
                enemy.OnDead += (enemy) => { combatingEnemies.Remove(enemy.InstanceID); Debug.Log(combatingEnemies.Count); };
                allie.OnDead += (allie) => { combatingEnemies.Remove(allie.InstanceID); Debug.Log(combatingAllies.Count); };

                if (!combatingAllies.ContainsKey(allie.GetInstanceID()))
                {
                    combatingAllies.Add(allie.GetInstanceID(), allie);

                }
                if(!combatingEnemies.ContainsKey(enemy.GetInstanceID()))
                {
                    combatingEnemies.Add(enemy.GetInstanceID(), enemy);

                }

                var status = new CombatStatus(enemy, allie,allie.transform.position,enemy.transform.position,0.11f);

                enemy.CombatStatus = status;
                allie.CombatStatus = status;

                combats.Add(status);
                return;
            }


            if(allie.CombatStatus != null)
            {
                allie.CombatStatus.JoinCombat(enemy);
                var status=allie.CombatStatus;
                enemy.CombatStatus = status;
                enemy.OnDead += (enemy) => { combatingEnemies.Remove(enemy.InstanceID); Debug.Log(combatingEnemies.Count); };
                if (!combatingEnemies.ContainsKey(enemy.GetInstanceID()))
                {
                    combatingEnemies.Add(enemy.GetInstanceID(), enemy);

                }
            }
            else if (enemy.CombatStatus != null)
            {
                enemy.CombatStatus.JoinCombat(allie);
                var status = enemy.CombatStatus;
                allie.CombatStatus = status;
                allie.OnDead += (allie) => { combatingAllies.Remove(allie.InstanceID); Debug.Log(combatingAllies.Count); };
                combatingAllies.Add(allie.GetInstanceID(), allie);
            }

        }

        /// <summary>
        /// 更新战斗
        /// </summary>
        public void UpdateCombat()
        {
            if (combats != null && combats.Count > 0)
            {
                for (int i = combats.Count - 1; i >= 0; i--)
                {
                    if (!combats[i].UpdateCombatStatus(combatSpeed))
                    {
                        for (int j = combats[i].enemies.Count-1; j >=0 ; j--)
                        {
                            EnemyUnit uni = combats[i].enemies[j];
                            uni.interactable = true;
                            uni.CombatStatus = null;
                            uni.OnDead(uni);
                            uni.OnDead -= uni.OnDead;

                        }
                        //foreach(var enemy in combats[i].enemies)
                        //{
                        //}
                        for (int j = combats[i].allies.Count - 1; j >= 0; j--)
                        {
                            PlayerUnit uni = combats[i].allies[j];
                            uni.interactable = true;
                            uni.CombatStatus = null;
                            uni.OnDead(uni);
                            uni.OnDead -= uni.OnDead;

                        }


                        //TODO 结束战斗后的处理
                        foreach(var item in combats[i].combatDrops)
                        {
                            this.SendCommand<EnemyDropCommand>(new EnemyDropCommand(combats[i].freeEnemyPosition, item));


                        }

                        combats.Remove(combats[i]);
                    }
                }

            }
        }

        public void PauseCombat()
        {
            combatSpeed = 0;
        }

        public void ContinueCombat()
        {
            combatSpeed = 1f;
        }
    }

    [System.Serializable]
    public class CombatStatus:ICanSendCommand,ICanGetModel
    {
        public Vector2 freeEnemyPosition;
        public Vector2 freeAlliePosition;

        public List<EnemyUnit> enemies;
        public List<PlayerUnit> allies;

        float combatDelay = 0.11f;
        //float combatTimer = 0f;

        public List<ItemDropData> combatDrops = new List<ItemDropData>();


        public CombatStatus(List<EnemyUnit> enemies,List<PlayerUnit> allies)
        {
            this.enemies = enemies;
            this.allies = allies;


        }
        public CombatStatus(EnemyUnit enemy, PlayerUnit allie, Vector2 freeAlliePos, Vector2 freeEnemyPos, float delay)
        {
            //combatDrops.Add(new ItemDropData(0, 1));
            //↑测试
            
            enemies =new List<EnemyUnit> {enemy};
            allies=new List<PlayerUnit> {allie};

            enemy.OnDead += (enemy) => { enemies.Remove(enemy as EnemyUnit);  };
            allie.OnDead += (allie) => { allies.Remove(allie as PlayerUnit);  };

            enemy.OnDead += (enemy) =>
            {
                var model = this.GetModel<IPlayerDataModel>();
                float jud = Random.value;
                if (jud <= model.enemyDropProb)
                {
                    combatDrops.Add(new ItemDropData(model.enemyDropLevel,1));
                }
            };

            freeEnemyPosition = freeEnemyPos;
            freeAlliePosition = freeAlliePos;
            combatDelay = delay;
        }

        public IArchitecture GetArchitecture()
        {
            return ApeEvolutionArchitecture.Interface;
        }

        /// <summary>
        /// 将敌人加入战斗
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public bool JoinCombat(EnemyUnit enemy)
        {
            Debug.Log("enemy"+enemy.name);

            freeEnemyPosition.x += 2.5f;
            enemy.interactable = false;
            enemy.CombatData.timer -= combatDelay;
            enemy.TranslatePosition(freeEnemyPosition);
            //敌人掉落
            enemy.OnDead += (enemy) => { enemies.Remove(enemy as EnemyUnit); Debug.Log(enemies.Count); };
            enemies.Add(enemy);

            return true;
        }
        /// <summary>
        /// 将友军加入战斗
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public bool JoinCombat(PlayerUnit allie)
        {
            freeAlliePosition.x += 2.5f;
            allie.interactable = false;
            allie.CombatData.timer -= combatDelay;
            allie.TranslatePosition(freeAlliePosition);
            allie.OnDead += (allie) => { allies.Remove(allie as PlayerUnit); Debug.Log(enemies.Count); };
            allies.Add(allie);
            return true;
        }

        
        public bool UpdateCombatStatus(float timeScale)
        {
            //combatTimer += Time.deltaTime*timeScale;

            for (int i = allies.Count - 1; i >= 0; i--)
            {
                UnitData data = allies[i].CombatData;
                data.timer += Time.deltaTime * timeScale;
                if (data.timer >= data.ATKSpeed)
                {
                    data.timer -= data.ATKSpeed;

                    var opponent = enemies[Random.Range(0, enemies.Count)];
                    allies[i].PunchAnimate(opponent.transform.position);

                    int oppoID = opponent.CombatData.InstanceID;
                    this.SendCommand<DealDamageCommand>(new DealDamageCommand(data.ATK, true, oppoID));
                }

            }
            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                UnitData data = enemies[j].CombatData;
                data.timer += Time.deltaTime * timeScale;
                if (data.timer >= data.ATKSpeed)
                {
                    data.timer -= data.ATKSpeed;

                    var opponent = allies[Random.Range(0, allies.Count)];
                    enemies[j].PunchAnimate(opponent.transform.position);

                    int oppoID = opponent.CombatData.InstanceID;


                    this.SendCommand<DealDamageCommand>(new DealDamageCommand(data.ATK, false, oppoID));

                }
            }

            //if (combatTimer > combatDelay)
            //{

            //}

            if (enemies.Count <= 0 || allies.Count <= 0)
            {
                return false;
            }
            return true;
        }


    }


}

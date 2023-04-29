using JetBrains.Annotations;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ApeEvolution
{
    public class MixedManager : ApeEvolutionController,IUtility,ICanSendEvent
    {
        [SerializeField]
        EnemyGeneratePoints generatePoints;

        [SerializeField]
        PlayerUnit playUnitPrefab;

        public static MixedManager Instance;
        [SerializeField]
        ChoiceUIControl choiceUIControl;

        [SerializeField]
        WinWindowUIControl winWindowUIControl;

        [SerializeField]
        LoseWindowControl loseWindowUIControl;

        [SerializeField]
        PlayerUnitFactory PlayerUnitFactory;

        [SerializeField]
        EnemyUnitFactory EnemyUnitFactory;

        [SerializeField]
        ItemDropFactory ItemDropFactory;



        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            this.RegisterEvent<EnemyWaveEvent>((e) =>
            {
                StartCoroutine(GenerateWave());

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ChoiceTypeEvent>((e) =>
            {
                choiceUIControl.gameObject.SetActive(true);
                choiceUIControl.PopUpWindow();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<GameWinEvent>((e) =>
            {
                winWindowUIControl.gameObject.SetActive(true);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<GameLoseEvent>((e) =>{
                loseWindowUIControl.gameObject.SetActive(true);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //this.SendEvent<EnemyWaveEvent>();
        }
        void Update()
        {
            this.GetSystem<ICombatSystem>().UpdateCombat();

        }

        public static GameObject[] GetPlayerUnits()
        {
            GameObject[] res = GameObject.FindGameObjectsWithTag("PlayerUnit");

            return res;
        }
        public ItemDrop GenerateItemDrop(Vector2 pos,ItemDropData data)
        {
            var instance = ItemDropFactory.Get(data.itemID);
            //this.GetSystem<ICardSystem>().AddPlayerCard(instance.transform)
            instance.transform.position = pos;
            return instance;
        }
        public void RecycleEnemy(EnemyUnit enemy)
        {
            EnemyUnitFactory.Reclaim(enemy);
        }
        public void RecyclePlayerUnit(PlayerUnit allie)
        {
            PlayerUnitFactory.Reclaim(allie);
        }

        public EnemyUnit GenerateEnemyUnit(Vector2 pos,int level)
        {
            EnemyUnit val = EnemyUnitFactory.Get(level);

            val.transform.position = pos;

            return val;
        }

        public PlayerUnit GeneratePlayerUnit(Vector2 pos)
        {
            PlayerUnit val= PlayerUnitFactory.Get(0);


            var model = this.GetModel<IPlayerDataModel>();

            val.CombatData = new PlayerUnitData(val.InstanceID, val, model.ethticHPInt.Value, model.ethticATKInt.Value, model.ethticATKSpd.Value);
            val.UpdateVisual();

            val.transform.position = pos;

            return val;
        }
        private IEnumerator GenerateWave()
        {

            int[] temp = this.GetModel<IEnemyWaveModel>().GetEnemyWave();

            for (int i = 0; i < temp.Length; i++)
            {
                var unit = EnemyUnitFactory.Get(temp[i] - 1);
                Vector2 pos = generatePoints.GetRandomSpawnPoint();
                unit.transform.position = pos;
                yield return new WaitForSeconds(0.3f);
            }
            yield return null;
        }

    }

    public struct EnemyWaveEvent
    {

    }
}
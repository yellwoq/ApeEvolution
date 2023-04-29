using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

namespace ApeEvolution
{
    //*************
    //Author:HUANG
    //Time:2023-2-12
    //时间系统
    //**********
    public interface ITimeSystem : ISystem,ICanSendCommand
    {
        float CurrentSeconds { get; }
        int OneDayTimeSeconds { get; set; }
        BindableProperty<CurrentCulture> MCurrentCulture { get; }
        void AddDelayTask(float seconds, Action OnFinished);
        int CalculateCurrentDay();
        string CalculateDayCurrentGameTime();
        void ResetTime();
    }
    public enum DelayTaskState
    {
        NotStart,
        Started
    }

    public class DelayTask
    {
        public float Seconds;

        public DelayTaskState State;

        public Action OnFinished;
        public float StartedSeconds;
        public float FinishedSeconds;
    }
    public enum CurrentCulture
    {
        IKariam,
        StoneAge,
        Ancient
    }
    public class TimeUpdateEvent
    {
        public string timeShowInfo;
    }
    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        public class TimeSystemUpdateBehaviour : MonoBehaviour
        {
            public event Action OnUpdate;

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }
        public class TimeSystemStartBehaviour : MonoBehaviour
        {
            public event Action OnStart;

            private void Start()
            {
                OnStart?.Invoke();
            }
        }
        public float CurrentSeconds { get; private set; } = 0;
        public BindableProperty<CurrentCulture> MCurrentCulture { get; private set; } = new BindableProperty<CurrentCulture>(CurrentCulture.IKariam);
        public int OneDayTimeSeconds { get; set; } = 3 * 60;
        private bool isLockEnemyCreate = false;

        private LinkedList<DelayTask> mDelayTasks = new LinkedList<DelayTask>();

        private GameObject updateTimeBehaviourObj;
        public void AddDelayTask(float seconds, Action OnFinished)
        {
            var delayTask = new DelayTask()
            {
                Seconds = seconds,
                State = DelayTaskState.NotStart,
                OnFinished = OnFinished
            };
            mDelayTasks.AddLast(delayTask);
        }
        private void Start()
        {
            this.GetModel<IExperienceModel>().CurrentExprience.RegisterWithInitValue(levelValue=>
            {
                if (levelValue == 0)
                {
                    MCurrentCulture.Value = CurrentCulture.IKariam;
                }
                else if (levelValue == 4)
                {
                    MCurrentCulture.Value = CurrentCulture.StoneAge;
                }
                else if (levelValue == 10)
                {
                    MCurrentCulture.Value = CurrentCulture.Ancient;
                    AddDelayTask(2 * OneDayTimeSeconds, () =>
                    {
                        //TODO
                        Debug.Log("产生怪物BOSS");
                    });
                }
            }).UnRegisterWhenGameObjectDestroyed(updateTimeBehaviourObj);
        }
        private void Update()
        {
            CurrentSeconds += Time.deltaTime;
            TaskHandle();
            CurrentTimeShowHandle();
        }

        private void TaskHandle()
        {
            if (mDelayTasks.Count > 0)
            {
                var currentTaskNode = mDelayTasks.First;
                while (currentTaskNode != null)
                {
                    var delayTask = currentTaskNode.Value;
                    var nextTaskNode = currentTaskNode.Next;
                    if (delayTask.State == DelayTaskState.NotStart)
                    {
                        delayTask.StartedSeconds = CurrentSeconds;
                        delayTask.FinishedSeconds = CurrentSeconds + delayTask.Seconds;
                        delayTask.State = DelayTaskState.Started;
                    }
                    else if (delayTask.State == DelayTaskState.Started)
                    {
                        if (CurrentSeconds >= delayTask.FinishedSeconds)
                        {
                            delayTask.OnFinished?.Invoke();
                            delayTask.OnFinished = null;
                            mDelayTasks.Remove(currentTaskNode);
                        }
                    }
                    currentTaskNode = nextTaskNode;
                }
            }
        }
        protected override void OnInit()
        {
            updateTimeBehaviourObj = new GameObject(nameof(TimeSystemUpdateBehaviour));
            UnityEngine.Object.DontDestroyOnLoad(updateTimeBehaviourObj);
            var timeUpdateBehaviour = updateTimeBehaviourObj.AddComponent<TimeSystemUpdateBehaviour>();
            var timeStartBehaviour = updateTimeBehaviourObj.AddComponent<TimeSystemStartBehaviour>();
            timeUpdateBehaviour.OnUpdate += Update;
            timeStartBehaviour.OnStart += Start;
            ResetTime();
        }
        private void CurrentTimeShowHandle()
        {
            int CurrentDay = CalculateCurrentDay();
            string CurrentDayCurrentGameTime = CalculateDayCurrentGameTime();
            string dayTimeShowInfo = $"{CurrentDayCurrentGameTime} 第{CurrentDay+1}天";
            this.SendEvent(new TimeUpdateEvent() { timeShowInfo = dayTimeShowInfo });
        }
        private void CurrentDisasterHandle()
        {
            IDisasterSystem disasterSystem = this.GetSystem<IDisasterSystem>();
            int currentPercentValue = Random.Range(1, 101);
            if (currentPercentValue <= disasterSystem.HappenPrecent.Value)
            {
                string[] disasterNameList=Enum.GetNames(typeof(DisasterType));
                string disasterCHName = "";
                int hanppenDisasterTypeValue = Random.Range(1, 101);
                for (int i = 0; i < disasterNameList.Length; i++)
                {
                    int maxValue = (i + 1) * 100 / disasterNameList.Length;
                    if (i == disasterNameList.Length - 1)
                    {
                        maxValue = 100;
                    }
                    if (hanppenDisasterTypeValue >= i * 100 / disasterNameList.Length
                        && hanppenDisasterTypeValue < maxValue)
                    {
                        DisasterType currentDisasterType = (DisasterType)Enum.Parse(typeof(DisasterType), disasterNameList[i]);
                        switch (currentDisasterType)
                        {
                            case DisasterType.Storm:
                                disasterCHName = "暴风雨";
                                break;
                            case DisasterType.Tornadoes:
                                disasterCHName = "龙卷风";
                                break;
                            case DisasterType.MonsterInvasion:
                                disasterCHName = "怪物入侵";
                                break;
                        }
                        Debug.Log("开始发生灾难+" + disasterCHName);
                        disasterSystem.HappenDisaster(currentDisasterType);
                        AddDelayTask(OneDayTimeSeconds, () =>
                        {
                             disasterSystem.CancelDisaster(currentDisasterType);
                             this.SendEvent(new DisasterShowEvent()
                             {
                                 showState = false,
                                 disasterName = ""
                             });
                            AddDelayTask(Random.Range(3, 8) * OneDayTimeSeconds, CurrentDisasterHandle);
                        });
                        break;
                    }
                }
                this.SendEvent(new DisasterShowEvent()
                {
                    showState=true,
                    disasterName = disasterCHName
                });
            }
            else
            {
                AddDelayTask(Random.Range(3, 8) * OneDayTimeSeconds, CurrentDisasterHandle);
            }
           
        }
        public int CalculateCurrentDay()
        {
            return CalculateDayBySeconds(CurrentSeconds);
        }
        private int CalculateDayBySeconds(float seconds)
        {
            return (int)(seconds / OneDayTimeSeconds);
        }
        public string CalculateDayCurrentGameTime()
        {
            int currentDay = CalculateCurrentDay();
            float timeExpandRatio = 24 * 3600 / OneDayTimeSeconds * 1.0f;
            float remainSeconds = (CurrentSeconds - currentDay * OneDayTimeSeconds)*timeExpandRatio;

            int currentGameHours = (int)(remainSeconds / 3600);
            List<int> createEnemyHours = new List<int>() { 2,8,14,20};
            int currentGameMins = (int)((remainSeconds - currentGameHours * 3600)/60);

            //if (currentGameMins == 0 && currentGameHours == 23) this.SendCommand<ConsumeFoodCommand>(new ConsumeFoodCommand());

            if (createEnemyHours.Contains(currentGameHours) && currentGameMins == 0)
            {
                if (!isLockEnemyCreate)
                {
                    //TODO
                    Debug.Log("开始生成敌人");
                    //发送产生敌人卡牌事件
                    isLockEnemyCreate = true;
                    this.SendEvent<EnemyWaveEvent>();
                }
            }
            else
            {
                isLockEnemyCreate = false;
            }
            return $"{currentGameHours.ToString().PadLeft(2,'0')}:{currentGameMins.ToString().PadLeft(2, '0')}";
        }

        public void ResetTime()
        {
            CurrentSeconds = 0;
            AddDelayTask(Random.Range(3, 8) * OneDayTimeSeconds, CurrentDisasterHandle);
        }
    }
}

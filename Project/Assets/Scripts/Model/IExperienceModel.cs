using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;


public interface IExperienceModel:IModel
{
    BindableProperty<int> CurrentExprience { get; }
    BindableProperty<int> CurrentLevel { get; }
    BindableProperty<int> LevelExprienceCost { get; }

    void UpgradeLevel();

}

public struct ChoiceTypeEvent
{

}
public struct CardPackTypeEvent
{

}
public struct ResourceTypeEvent
{

}

public class ExperienceModel : AbstractModel, IExperienceModel
{
    public BindableProperty<int> CurrentExprience { get; }=new BindableProperty<int>();
    public BindableProperty<int> LevelExprienceCost { get; } = new BindableProperty<int>();

    public BindableProperty<int> CurrentLevel { get; }=new BindableProperty<int>();

    LinkedList<LevelInfo> levelInfos = new LinkedList<LevelInfo>();

    LinkedListNode<LevelInfo> currentNode;

    protected override void OnInit()
    {
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));
        levelInfos.AddLast(new LinkedListNode<LevelInfo>(new LevelInfo(100, ReachAwardType.Choice)));


        currentNode = levelInfos.First;
        CurrentLevel.Value = 1;
        LevelExprienceCost.Value=currentNode.Value.ExpCost;
        CurrentExprience.Value = 0;
        CurrentLevel.Register((int e) =>
        {
            currentNode = currentNode.Next;

            LevelExprienceCost.Value = currentNode.Value.ExpCost;

            switch (currentNode.Value.AwardType)
            {
                case ReachAwardType.Choice:
                    this.SendEvent<ChoiceTypeEvent>();
                    break;
                case ReachAwardType.CardPack:
                    break;
                case ReachAwardType.Resource:
                    break;
            }
        });
    }

    public void UpgradeLevel()
    {
        CurrentLevel.Value += 1;
    }

    public class LevelInfo
    {

        public int ExpCost = 0;

        public ReachAward reach;

        public ReachAwardType AwardType;

        public LevelInfo(int _cost,ReachAwardType type,ReachAward reach=null)
        {

            AwardType = type;
            ExpCost =_cost;
        }

    }

    public enum ReachAwardType
    {
        Choice,
        CardPack,
        Resource
    }

    public class ReachAward
    {

    }
}

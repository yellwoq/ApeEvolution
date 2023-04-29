using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public interface IChoiceModel:IModel
    {
        ChoiceCard GetChoiceCard();
    }
    public class ChoiceModel : AbstractModel, IChoiceModel
    {
        private List<ChoiceCard> _legendCards=new List<ChoiceCard>();
        private List<ChoiceCard> _epicCards=new List<ChoiceCard>();
        private List<ChoiceCard> _rareCards=new List<ChoiceCard>();

        private float legendRate = 0.1f;
        private float epicRate = 0.4f;


        public ChoiceCard GetChoiceCard()
        {
            float num=Random.value;
            if (num < legendRate)
            {
                return _legendCards[Random.Range(0, _legendCards.Count)];
            }
            else if(num<epicRate)
            {
                return _epicCards[Random.Range(0, _epicCards.Count)];
            }
            else
            {
                return _rareCards[Random.Range(0, _rareCards.Count)];
            }
        }

        protected override void OnInit()
        {
            _legendCards.Add(new ChoiceCard(it:IconType.Atk,elite:CardElite.Legend,aTKRate: 1.5f,description:"提升50%的伤害"));
            _rareCards.Add(new ChoiceCard(it:IconType.Atk,elite:CardElite.Rare,aTKRate: 1.2f,description:"提升20%的伤害"));
            _epicCards.Add(new ChoiceCard(it:IconType.Atk,elite:CardElite.Epic,aTKRate: 1.3f,description:"提升30%的伤害"));
            _legendCards.Add(new ChoiceCard(it:IconType.AtkSpd,elite: CardElite.Legend, aTKSpdRate: 1.5f, description: "提升50%攻击速度"));
            _epicCards.Add(new ChoiceCard(it:IconType.AtkSpd,elite: CardElite.Epic, aTKSpdRate: 1.3f, description: "提升30%攻击速度"));
            _rareCards.Add(new ChoiceCard(it:IconType.AtkSpd,elite: CardElite.Rare, aTKSpdRate: 1.1f, description: "提升10%攻击速度"));
            _legendCards.Add(new ChoiceCard(it:IconType.Hp,elite: CardElite.Legend, hPRate: 1.5f, description: "提升50%血量"));
            _epicCards.Add(new ChoiceCard(elite: CardElite.Epic, hPRate: 1.4f, description: "提升40%血量"));
            _rareCards.Add(new ChoiceCard(elite: CardElite.Rare, hPRate: 1.25f, description: "提升25%血量"));
            _rareCards.Add(new ChoiceCard(elite: CardElite.Rare, hPRate: 1.25f, description: "提升25%血量"));
            _rareCards.Add(new ChoiceCard(it: IconType.MatingSpd, elite: CardElite.Rare, matSpd:1.3f, description: "交配速度提升30%"));

            _legendCards.Add(new ChoiceCard(it: IconType.SynSpd, elite: CardElite.Legend, syn:1.4f, description: "合成速度加快40%"));
            _rareCards.Add(new ChoiceCard(it: IconType.SynSpd, elite: CardElite.Rare, syn:1.17f, description: "合成速度加快17%"));
            _epicCards.Add(new ChoiceCard(it: IconType.SynSpd, elite: CardElite.Epic, syn: 1.3f, description: "合成速度加快30%"));
        }
    }
    public class ChoiceCard
    {
        //攻击
        public float ATKRate;
        //攻速
        public float ATKSpdRate;
        public float HPRate;
        public IconType icon;
        //合成速度
        public float SynSpd;
        //经验获取百分比
        public float ExpObtRate;
        //生育速度
        public float MatingSpd;

        public CardElite Elite;

        public string Description;

        public ChoiceCard(float syn=1.0f,float exp=1.0f,float matSpd = 1.0f,IconType it=IconType.Hp,CardElite elite=CardElite.Rare,float aTKRate = 1.0f, float aTKSpdRate = 1.0f, float hPRate = 1.0f, string description = "没有介绍")
        {
            icon = it;
            Elite = elite;
            ATKRate = aTKRate;
            ATKSpdRate = aTKSpdRate;
            HPRate = hPRate;
            Description = description;
            SynSpd= syn;
            ExpObtRate = exp;
            MatingSpd = matSpd;
        }
    }

    public enum CardElite
    {
        Legend,
        Epic,
        Rare
    }
    public enum IconType
    {
        Atk,
        AtkSpd,
        MatingSpd,
        SynSpd,
        Hp,
        ExpObtainRate
    }
}
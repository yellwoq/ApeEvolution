using JetBrains.Annotations;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
    public interface IGenerateSystem : ISystem,ICanSendCommand
    {
        void Copulation(UnitData A, UnitData B);
        void GenerateItemDrop(Vector2 position, ItemDropData data);
    }

    public class GenerateSystem : AbstractSystem, IGenerateSystem
    {
        protected override void OnInit()
        {

        }

        public void Copulation(UnitData A,UnitData B)
        {

        }
        public void GenerateItemDrop(Vector2 position,ItemDropData data)
        {
            var val = MixedManager.Instance.GenerateItemDrop(position, data);
            this.GetSystem<ICardSystem>().AddPlayerCard(position, val.GetComponent<FoodCard>());
            this.SendCommand<AddExprienceCommand>(new AddExprienceCommand(10));
        }
        public void GeneratePlayerUnit(Vector2 position)
        {
            MixedManager.Instance.GeneratePlayerUnit(position);
        }


    }
}
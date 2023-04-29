using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ApeEvolution
{
    public struct GameLoseEvent
    {

    }
    public struct GameWinEvent
    {

    }
    public interface IGameStateSystem
    {
        void CheckLose();
    }
    public class GameStateSystem : AbstractSystem, IGameStateSystem
    {
        public void CheckLose()
        {
            var val = MixedManager.GetPlayerUnits();
            if (val.Length <= 0)
            {
                this.SendEvent<GameLoseEvent>();
            }
        }


        protected override void OnInit()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using ApeEvolution;

public class FinalEnemy : MonoBehaviour, ICanSendEvent
{
    public IArchitecture GetArchitecture()
    {
        return ApeEvolutionArchitecture.Interface;
    }

    private void OnDestroy()
    {
        this.SendEvent<GameWinEvent>();
    }
}

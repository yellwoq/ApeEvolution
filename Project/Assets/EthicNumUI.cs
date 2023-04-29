using ApeEvolution;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EthicNumUI : MonoBehaviour,ICanSendEvent
{
    [SerializeField]
    TextMeshProUGUI _numText;

    public IArchitecture GetArchitecture()
    {
        return ApeEvolutionArchitecture.Interface;
    }

    private void Update()
    {
        int v = MixedManager.GetPlayerUnits().Length;
        if (v <= 0)
        {
            this.SendEvent<GameLoseEvent>();

        }
        _numText.text =v.ToString();
    }
}

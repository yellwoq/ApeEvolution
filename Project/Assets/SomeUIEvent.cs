using ApeEvolution;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SomeUIEvent : ApeEvolutionController
{
    [SerializeField]
    TextMeshProUGUI curText;
    [SerializeField]
    TextMeshProUGUI needText;

    public void BackToHome()
    {
        SceneManager.LoadScene("GameStartUI");
    }
    private void Start()
    {
        this.GetModel<IPlayerDataModel>().foodStore.RegisterWithInitValue((e) =>
        {
            curText.text = e.ToString();
        });
    }
    private void Update()
    {
        needText.text = (MixedManager.GetPlayerUnits().Length * 2).ToString();
    }

}

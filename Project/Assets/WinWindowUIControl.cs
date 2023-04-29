using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ApeEvolution
{

    public class WinWindowUIControl : ApeEvolutionController
    {
        [SerializeField]
        Button AdmitButton;
        [SerializeField]
        TextMeshProUGUI surviveTime;

        private void OnEnable()
        {
            AdmitButton.onClick.AddListener(BackToHomeScene);
            surviveTime.text = this.GetSystem<ITimeSystem>().CalculateDayCurrentGameTime();
        }

        private void BackToHomeScene()
        {
            Debug.Log("回到主页面");
        }


    }
}
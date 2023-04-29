using ApeEvolution;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUITest : ApeEvolutionController
{
    private IExperienceModel _expModel;

    [SerializeField]Slider m_Slider;
    [SerializeField] TextMeshProUGUI _LevelText;

    private void Start()
    {
        _expModel=this.GetModel<IExperienceModel>();

        _expModel.CurrentExprience.RegisterWithInitValue((int a) =>
        {
            float val = (float)a / _expModel.LevelExprienceCost.Value;

            m_Slider.value = val;
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        _expModel.LevelExprienceCost.RegisterWithInitValue((a) =>
        {
            float val = (float)_expModel.CurrentExprience.Value / a;

            m_Slider.value = val;
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        _expModel.CurrentLevel.RegisterWithInitValue((a) =>
        {
            _LevelText.text = a.ToString();
        });

    }

}

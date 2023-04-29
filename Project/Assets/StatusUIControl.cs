using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ApeEvolution
{
    public class StatusUIControl : ApeEvolutionController
    {
        public TextMeshProUGUI TimeText;

        public TextMeshProUGUI CoinText;

        public TextMeshProUGUI CurFoodText;

        public TextMeshProUGUI NeedFoodText;

        private void Start()
        {
            this.GetSystem<IStoreSystem>().CurrentMoney.RegisterWithInitValue((e) =>
            {
                CoinText.text = e.ToString();
            });
        }
    }
}
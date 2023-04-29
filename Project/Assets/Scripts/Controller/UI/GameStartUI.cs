using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ApeEvolution
{
    /// <summary>
    /// 游戏开始界面UI
    /// </summary>
    public class GameStartUI:ApeEvolutionController
    {
        public GameSetUI gameSetUI;

        [SerializeField]
        RectTransform GameName;
        [SerializeField]
        RectTransform PlayBtn;

        public void StartGameClick()
        {
            Debug.Log("ho");
            SceneManager.LoadScene("GameScene");

        }
        public void SetWindowOpenClick()
        {

        }

        private void Update()
        {

            //GameName.DOScale(1.1f, 0.6f).SetLoops(-1, LoopType.Yoyo);

            //PlayBtn.DOScale(1.1f, 0.6f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}

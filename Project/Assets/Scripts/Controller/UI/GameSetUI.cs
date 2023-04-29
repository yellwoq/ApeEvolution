using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ApeEvolution
{
    /// <summary>
    /// 游戏设置界面
    /// </summary>
    public class GameSetUI:MonoBehaviour
    {
        public GameStartUI startUI;
        public AudioSource bgmSource;
        public AudioSource sfxSource;
        public Slider bgmSlider;
        public Slider sfxSlider;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void OnEnable()
        {
            bgmSlider.value = PlayerPrefs.GetFloat("bgm");
            sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        }
       
        public void SetBgVoice(float v)
        {
            bgmSource.volume = v;
            PlayerPrefs.SetFloat("bgm", v);
        }
        public void SetSfxVoice(float v)
        {
            sfxSource.volume = v;
            PlayerPrefs.SetFloat("sfx", v);
        }
        public void SetWindowOpenClick()
        {
            startUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace CDPC.GameManagers
{
    public class PauseGame : MonoBehaviour
    {
        public static PauseGame instance;

        public GameObject PauseUI;
        public GameObject BtnPauseUI;
        public GameObject MenuSettingUI;
        public bool isPause = false;
        [Header("Audio")]
        public AudioMixer audioMixer;
        public Resolution[] resolutions;
        public Dropdown resolutionDropDown;
        public Slider getVolume;
        private LoadingScreen loadingScreen;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            loadingScreen = LoadingScreen.instance;
            getVolume.value = PlayerPrefs.GetFloat("volumes");
            PauseUI.SetActive(false);
            BtnPauseUI.SetActive(true);
            OnSetUpResolution();
        }

        private void OnSetUpResolution()
        {
            resolutions = Screen.resolutions;
            resolutionDropDown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropDown.AddOptions(options);
            resolutionDropDown.value = currentResolutionIndex;
            resolutionDropDown.RefreshShownValue();
        }

        void Update()
        {
            if (isPause)
            {
                BtnPauseUI.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                BtnPauseUI.SetActive(true);
                Time.timeScale = 1;
            }
        }
        public void PauseGames()
        {
            isPause = true;
            PauseUI.SetActive(true);
        }
        public void Resume()
        {
            isPause = false;
            PauseUI.SetActive(false);
        }
        public void Restart()
        {
            loadingScreen.LoadScreen(SceneManager.GetActiveScene().buildIndex);
        }

        public void SelectLevel()
        {
            loadingScreen.LoadScreen(0);
        }
        public void Quit()
        {
            Application.Quit();
        }

        //Setting Option
        public void MainMenu()
        {
            isPause = true;
            MenuSettingUI.SetActive(false);
            PauseUI.SetActive(true);
        }

        public void MenuSetting()
        {
            isPause = true;
            MenuSettingUI.SetActive(true);
            PauseUI.SetActive(false);
        }

        public void SetMasterVolume(float volume)
        {
            PlayerPrefs.SetFloat("volumes", volume);
            audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volumes"));
        }

        public void SetMusicVolume(float value)
        {

        }
        public void SetSFXVoume()
        {

        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }
        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}

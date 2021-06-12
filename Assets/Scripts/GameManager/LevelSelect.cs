using UnityEngine;
using UnityEngine.UI;

namespace CDPC.GameManagers
{
    public class LevelSelect : MonoBehaviour
    {
        [SerializeField]
        private bool Unlock;
        public Image unlockImage;
        public GameObject[] star;
        public Sprite starSprite;
        private LoadingScreen loadingScreen;

        private void Start()
        {
            loadingScreen = LoadingScreen.instance;
        }

        public void Update()
        {
            UpdateLevel();
            UpdateLevelStatus();
        }

        public void UpdateLevelStatus()
        {
            int previousLevelNum = int.Parse(gameObject.name) - 1;
            if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)
            {
                Unlock = true;
            }
        }

        public void UpdateLevel()
        {
            if (!Unlock)
            {
                unlockImage.gameObject.SetActive(true);
                for (int i = 0; i < star.Length; i++)
                {
                    star[i].gameObject.SetActive(false);
                }
            }
            else
            {
                unlockImage.gameObject.SetActive(false);
                for (int i = 0; i < star.Length; i++)
                {
                    star[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
                {
                    star[i].gameObject.GetComponent<Image>().sprite = starSprite;
                }
            }
        }
        public void PressSelection(int _levelName)
        {
            if (Unlock)
            {
                loadingScreen.LoadScreen(_levelName);
            }
        }
    }
}
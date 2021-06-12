using UnityEngine;
using UnityEngine.UI;
using CDPC.GameManagers;

namespace CDPC.Mechanic
{
    public class Door : MonoBehaviour
    {
        public Text txtEndGame;
        public GameObject Notification;
        public bool isDisplay = false;
        private int currentStarsNum;
        public int levelIndex;
        private GameManager gameManager;
        private LoadingScreen loadingScreen;

        private void Awake()
        {
            gameManager = GameManager.instance;
            loadingScreen = LoadingScreen.instance;
        }

        void Start()
        {
            currentStarsNum = 0;
            Notification.SetActive(false);
        }

        private void Update()
        {
            if (isDisplay)
            {
                Notification.SetActive(true);
                txtEndGame.text = "Through the game screen";
            }
            if (!isDisplay)
            {
                Notification.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isDisplay = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isDisplay = false;
            }
        }
        public void EndGame()
        {
            if (gameManager.score >= 10 && gameManager.score < 30)
            {
                currentStarsNum = 1;
                PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
            }
            if (gameManager.score >= 30 && gameManager.score < 50)
            {
                currentStarsNum = 2;
                PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
            }
            if (gameManager.score >= 50)
            {
                currentStarsNum = 3;
                PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
            }
            loadingScreen.LoadScreen(0);
        }
    }
}

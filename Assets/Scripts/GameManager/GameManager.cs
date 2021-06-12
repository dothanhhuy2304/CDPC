using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CDPC.Player;

namespace CDPC.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private GameObject[] characterPrefab;
        [Header("Scores")]
        public int score = 0;
        public int hightScores = 0;
        [SerializeField] private Text txtHightScores;
        [SerializeField] private Text txtScorce;
        [Header("HealthUI")]
        [SerializeField] private Text txtHealth;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private Text txtDeath;
        [Header("TimeRemaining")]
        [SerializeField] private float timeRemaining = 120f;
        [HideInInspector]
        [SerializeField] private float startTimeRemaining = 120f;
        public Text txtTimeRemaining;
        [Header("CheckPoint")]
        public Vector2 lastCheckPointPos;
        private HealthController healthController;

        private void Awake()
        {
            instance = this;
            LoadCharacter();
        }
        private void Start()
        {
            healthController = HealthController.instance;
            SetScores();
        }

        private void SetScores()
        {
            txtHightScores.text = ("HightScores:" + PlayerPrefs.GetInt("hightScores"));
            hightScores = PlayerPrefs.GetInt("hightScores", 0);
            if (PlayerPrefs.HasKey("scores"))
            {
                Scene activeScreen = SceneManager.GetActiveScene();
                if (activeScreen.buildIndex == 1)
                {
                    PlayerPrefs.DeleteKey("scores");
                    score = 0;
                }
                else
                {
                    PlayerPrefs.GetInt("scores");
                    score = PlayerPrefs.GetInt("scores");
                }
            }
        }

        public void Scores(int scoress)
        {
            score += scoress;
        }
        private void Update()
        {
            SetUI();
            SetPlayerDeath();
        }

        private void SetUI()
        {
            txtScorce.text = score.ToString();
            txtHealth.text = healthController.health.ToString();
            //Set timeRemaining 
            timeRemaining -= Time.deltaTime;
            float timeCountDown = Mathf.Floor(timeRemaining);
            txtTimeRemaining.text = timeCountDown.ToString("0:00");
            if (timeCountDown <= 0)
            {
                healthController.Death();
            }
        }


        private void SetPlayerDeath()
        {
            if (healthController.isDeath)
            {
                gameUI.SetActive(true);
                txtDeath.text = "You are Death";
                timeRemaining = startTimeRemaining;
            }
            else
            {
                gameUI.SetActive(false);
            }
        }

        public void LoadCharacter()
        {
            int index = PlayerPrefs.GetInt("character");
            GameObject character = Instantiate(characterPrefab[index], new Vector3(-29.33f, -0.46f, 0), Quaternion.identity);
            character.name = "Player";
        }
    }
}

using System.Collections;
using UnityEngine;
using CDPC.GameManagers;

namespace CDPC.Player
{
    public class HealthController : MonoBehaviour
    {
        public static HealthController instance;
        public int health;
        public int maxHealth;
        public bool isDeath;
        private int currentHealth;
        private float currentSpeed;
        private GameManager gameManager;
        private PlayerController player;

        private void Awake()
        {
            instance = this;
            maxHealth = 5;
            isDeath = false;
            player = FindObjectOfType<PlayerController>();
            gameManager = GameManager.instance;
            currentHealth = health;
            currentSpeed = player.speed;
        }

        public void Heal(int value)
        {
            health += value;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void Hurt(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                Death();
            }
        }

        public void Death()
        {
            if (PlayerPrefs.GetInt("hightScores") < gameManager.score)
            {
                PlayerPrefs.SetInt("hightScores", gameManager.score);
            }
            StartCoroutine(Deaths(5f));
        }

        public IEnumerator Deaths(float time)
        {
            isDeath = true;
            player.speed = 0f;
            health = 0;
            yield return new WaitForSeconds(time);
            isDeath = false;
            player.speed = currentSpeed;
            health = currentHealth;
            player.transform.position = gameManager.lastCheckPointPos;
            yield return null;
        }
    }
}
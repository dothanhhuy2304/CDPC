using UnityEngine;
using CDPC.GameManagers;

namespace CDPC.Mechanic
{
    public class Gem : MonoBehaviour
    {
        [Header("Gem")]
        public LayerMask whatIsPlayer;
        public float radius;
        SoundManger soundManger;
        GameManager gameManager;

        private void Start()
        {
            soundManger = SoundManger.Instance;
            gameManager = GameManager.instance;
        }

        private void FixedUpdate()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
            if (hit)
            {
                if (hit.CompareTag("Player"))
                {
                    gameManager.Scores(5);
                    soundManger.audioSource.clip = soundManger.coins;
                    soundManger.audioSource.PlayOneShot(soundManger.coins);
                    Destroy(gameObject);
                }
            }
        }
    }
}

using UnityEngine;

namespace CDPC.GameManagers
{
    public class CheckPoint : MonoBehaviour
    {
        public ParticleSystem dust;
        GameManager gameManager;
        private void Start()
        {
            gameManager = GameManager.instance;
            dust.Play();
        }

        [System.Obsolete]
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                gameManager.lastCheckPointPos = transform.position;
                Color color = new Color(188, 61, 40, 255);
                dust.startColor = color;
            }
        }
    }
}

using UnityEngine;
using CDPC.Player;

namespace CDPC.Mechanic
{
    public class PlayerFalling : MonoBehaviour
    {
        const string Player = "Player";
        private HealthController healthController;

        private void Awake()
        {
            healthController = HealthController.instance;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Player))
            {
                StartCoroutine(healthController.Deaths(5f));
            }
        }
    }
}
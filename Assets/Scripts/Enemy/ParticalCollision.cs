using UnityEngine;
using CDPC.Player;

public class ParticalCollision : MonoBehaviour
{
    private HealthController healthController;
    private float timeDelay = 1.5f;
    private float startTime = 1.5f;
    private void Start()
    {
        healthController = HealthController.instance;
    }

    private void Update()
    {
        timeDelay -= Time.deltaTime;
        if (timeDelay <= 0)
        {
            timeDelay = 0;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeDelay == 0)
            {
                healthController.Hurt(1);
                timeDelay = startTime;
            }
        }
    }
}

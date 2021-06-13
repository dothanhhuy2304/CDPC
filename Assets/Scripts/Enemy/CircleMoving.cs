using UnityEngine;
using CDPC.Player;

namespace CDPC.Enemy
{
    public class CircleMoving : MonoBehaviour
    {
        public float minSpeed;
        public float maxSpeed;
        public float startWaitTime;
        private float waitTime;
        public Vector3 target;
        public Transform[] moveSpots;
        private int ramdomSpot;
        [SerializeField] ParticleSystem dust;
        public float timeDelay = 1.5f, startTime = 1.5f;
        private HealthController healthController;

        private void Start()
        {
            healthController = HealthController.instance;
            dust = gameObject.GetComponentInChildren<ParticleSystem>();
            waitTime = startWaitTime;
            ramdomSpot = Random.Range(0, moveSpots.Length);
            dust.Play();
        }

        private void Update()
        {
            Moving();
            DoAttack();
        }

        void Moving()
        {
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[ramdomSpot].position, randomSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpots[ramdomSpot].position) <= 0f)
            {
                if (waitTime <= 0)
                {
                    ramdomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        void DoAttack()
        {
            timeDelay -= Time.deltaTime;
            if (timeDelay <= 0)
            {
                timeDelay = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (timeDelay == 0)
                {
                    timeDelay = startTime;
                    healthController.Hurt(1);
                }
            }
        }
    }
}
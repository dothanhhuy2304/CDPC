using UnityEngine;
using CDPC.Player;

namespace CDPC.Mechanic
{
    public class Spikes : MonoBehaviour
    {
        const string Player = "Player";
        public float speed = 1;
        public Transform tagGround;
        public float distance = 0.5f;
        public LayerMask whatIsGround;
        public bool movingUp;

        public float timeDelay = 1.5f;
        public float startTime = 1.5f;
        private HealthController healthController;

        private void Awake()
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

        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(tagGround.transform.position, Vector2.up, distance, whatIsGround);
            if (hit.collider)
            {
                if (movingUp)
                {
                    transform.eulerAngles = new Vector3(-180, 0, 0);
                    movingUp = false;
                }
                else
                {
                    transform.eulerAngles = Vector3.zero;
                    movingUp = true;
                }
            }
            Moving();
        }

        void Moving()
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Player))
            {
                if (timeDelay == 0)
                {
                    healthController.Hurt(1);
                    timeDelay = startTime;
                }
            }
        }
    }
}
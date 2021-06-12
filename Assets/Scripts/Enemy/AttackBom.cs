using UnityEngine;
using CDPC.Player;

namespace CDPC.Enemy
{
    public class AttackBom : MonoBehaviour
    {
        public float speed;
        public float distance = 1f;
        Vector3 target;
        private Transform player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>().transform;
        }
        private void Start()
        {
            Invoke(nameof(DestroyBom), 2.5f);
            target = (player.position - transform.position).normalized;
        }



        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, distance);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("ground"))
                {
                    Destroy(gameObject);
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<HealthController>().Hurt(1);
                    Destroy(gameObject);
                }
            }
            transform.Translate(target * speed * Time.deltaTime);
        }

        void DestroyBom()
        {
            Destroy(gameObject);
        }
    }
}
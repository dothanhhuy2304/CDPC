using UnityEngine;

namespace CDPC.Enemy
{
    public class ActiveBoss : MonoBehaviour
    {
        public GameObject boss;
        public GameObject prefabWall;
        private void Start()
        {
            boss = FindObjectOfType<BossController>().gameObject;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                boss.GetComponent<BossController>().isRange = true;
                prefabWall.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
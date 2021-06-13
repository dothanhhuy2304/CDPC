using UnityEngine;
using CDPC.GameManagers;
using CDPC.Enemy;

namespace CDPC.Player
{
    public class Projectile : MonoBehaviour
    {
        [Header("FireProjectile")]
        public float speed;
        public float lifeTime;
        public float distance;
        public LayerMask whatIsSolid;
        public GameObject destroyProjectile;
        public ParticleSystem dustBoom;
        private SoundManger soundManger;
        private void Awake()
        {
            soundManger = SoundManger.Instance;
        }
        private void Start()
        {
            Invoke(nameof(DestroyProjectile), lifeTime);
        }
        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, distance, whatIsSolid);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyController>().TakeDamage(Random.Range(7, 18));
                    DestroyProjectile();
                }
                else if (hit.collider.CompareTag("ground"))
                {
                    DestroyProjectile();
                }
                else if (hit.collider.CompareTag("Boss"))
                {
                    hit.collider.GetComponent<BossController>().TakeDamage(Random.Range(30, 60));
                    DestroyProjectile();
                }
            }
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        void DestroyProjectile()
        {
            Instantiate(destroyProjectile, transform.position, Quaternion.identity);
            Destroy(gameObject);
            dustBoom.Stop();
            soundManger.audioSource.clip = soundManger.explosion;
            soundManger.audioSource.PlayOneShot(soundManger.explosion);
            //CameraShake.Instance.Shake(5f, .1f);
        }
    }
}

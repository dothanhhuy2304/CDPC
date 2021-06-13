using UnityEngine;
using CDPC.Player;
using CDPC.GameManagers;

namespace CDPC.Enemy
{
    public class BossController : MonoBehaviour
    {
        private Transform player;
        [SerializeField] private float speed = 5;
        private Rigidbody2D rb;
        Animator animator;
        float timeAttack = 3f;
        float startAttack = 3f;
        public GameObject pointAttack;
        public bool isRange = false;
        [Header("Skill 2")]
        private int waiting = 0;
        [SerializeField] private Vector2 spawn;
        public GameObject prefab;
        private HealthController healthController;
        private SoundManger soundManger;
        [Header("HealthSystem")]
        public int health;
        public int maxHealth = 800;
        private float timeHear = 10f, startTime = 10f;
        public GameObject floatingTextPrefab;
        bool isDeath = false;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = FindObjectOfType<PlayerController>().transform;
            healthController = HealthController.instance;
            soundManger = SoundManger.Instance;
            health = maxHealth;
            isDeath = false;
        }

        public void Hear(int value)
        {
            health += value;
            ShowDamage(value.ToString());
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            ShowDamage(damage.ToString());
            if (health <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            isDeath = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, 5f);
        }

        public void ShowDamage(string text)
        {
            Vector3 offset = new Vector3(-0.2f, 1, 0);
            GameObject textPrefab = Instantiate(floatingTextPrefab, transform.position + offset, Quaternion.identity);
            textPrefab.GetComponentInChildren<TextMesh>().text = text;
            textPrefab.name = "Damage";
        }

        private void Update()
        {
            timeHear -= Time.deltaTime;
            if (timeHear <= 0)
            {
                Hear(50);
                timeHear = startTime;
            }
        }

        private void FixedUpdate()
        {
            if (!isDeath)
            {
                if (!healthController.isDeath)
                {
                    if (isRange)
                    {
                        Moving();
                    }
                    if (!isRange)
                    {
                        animator.Play("Idle");
                    }
                }
                else
                {
                    transform.position = transform.position;
                    isRange = false;
                }
                timeAttack -= Time.deltaTime;
                if (timeAttack <= 0)
                {
                    timeAttack = 0;
                }
            }
        }

        void Moving()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            Vector3 directionPos = player.position - transform.position;
            if (distance < 50f && distance > 1.5f)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            if (distance <= 1.5f)
            {
                rb.velocity = Vector2.zero;
                RaycastHit2D hit = Physics2D.BoxCast(pointAttack.transform.position, new Vector2(10f, 1.1f), 0, Vector2.one * 3, 10f, 1 << LayerMask.NameToLayer("Player"));

                if (timeAttack == 0)
                {
                    if (hit)
                    {
                        hit.collider.GetComponent<HealthController>().Hurt(1);
                    }
                    animator.SetTrigger("Attack");
                    soundManger.audioSource.clip = soundManger.swords;
                    soundManger.audioSource.PlayOneShot(soundManger.swords);
                    timeAttack = startAttack;
                    waiting++;
                }
                if (waiting > 3)
                {
                    float posX = Random.Range(player.transform.position.x, transform.position.x);
                    spawn = new Vector2(posX, 5.0f);
                    GameObject sword = Instantiate(prefab, spawn, Quaternion.Euler(-180, 0, 0));
                    sword.name = "Kiem :)";
                    waiting = 0;
                }
            }
            Flip(directionPos.x);
        }

        void Flip(float pos)
        {
            float d = pos;
            if (d < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (d > 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }

    }
}
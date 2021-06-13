using UnityEngine;
using CDPC.Player;
using CDPC.GameManagers;

namespace CDPC.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Animator")]
        public Animator animator;
        [Header("Health")]
        [SerializeField] private float health;
        [SerializeField] private int maxHealth;
        public HeathBarUI heathBar;
        public GameObject floatingTextPrefab;
        [Header("Moving")]
        [SerializeField]
        Rigidbody2D rb;
        public Transform tagGround;
        public Transform checkGround;
        public float speed;
        private bool movingRight = false;
        private float distance = 3f;
        [Header("Attack")]
        public GameObject bom;
        public float minBomTime = 2f, maxBomTime = 5f;
        public float timeStart = 0f;
        public float minTimeShot = 1.5f, maxTimeShot = 5f;
        [Header("DistanceAttack")]
        [SerializeField]
        LayerMask whatIsPlayer;
        public float radius = 6.5f;
        public bool isMoving;
        private Transform player;
        private float rangeAttack;
        private GameManager gameManager;
        private HealthController healthController;

        public float Health { get => health; set => health = value; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            isMoving = true;
        }
        private void Start()
        {
            Health = maxHealth;
            heathBar.SetHealth(Health, maxHealth);
            rb = GetComponent<Rigidbody2D>();
            player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>().transform;
            gameManager = GameManager.instance;
            healthController = HealthController.instance;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            heathBar.SetHealth(Health, maxHealth);
            ShowDamage(damage.ToString());
            if (Health <= 0)
            {
                gameManager.Scores(50);
                Dead();
            }
        }
        public void ShowDamage(string text)
        {
            Vector3 offset = new Vector3(-0.2f, 1, 0);
            GameObject textPrefab = Instantiate(floatingTextPrefab, transform.position + offset, Quaternion.identity);
            textPrefab.GetComponentInChildren<TextMesh>().text = text;
            textPrefab.name = "Damage";
        }

        public void Dead()
        {
            isMoving = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(tagGround.position, Vector2.down, distance, 1 << LayerMask.NameToLayer("ground"));
            bool hitt = Physics2D.Raycast(checkGround.position, Vector2.zero, 0, 1 << LayerMask.NameToLayer("ground"));
            if (isMoving)
            {
                if (!hit.collider || hitt)
                {
                    if (movingRight)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingRight = false;
                    }
                    else
                    {
                        transform.eulerAngles = Vector3.zero;
                        movingRight = true;
                    }
                }
            }
            Moving();
            Attack();
        }
        void Moving()
        {
            rangeAttack = Vector3.Distance(transform.position, player.position);
            if (rangeAttack > 6.5f || healthController.isDeath)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                animator.SetBool("Attack", false);
            }
            else if (rangeAttack < 6.5f)
            {
                animator.SetBool("Attack", true);
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.x, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(transform.rotation.x, angle, 1 * Time.deltaTime);
                transform.position = transform.position;
            }
        }

        void Attack()
        {
            bool hitDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
            if (hitDetected)
            {
                if (!healthController.isDeath)
                {

                    PrefabBom();
                }
            }
        }

        void PrefabBom()
        {
            timeStart += Time.deltaTime;
            if (timeStart >= Random.Range(minTimeShot, maxTimeShot))
            {
                Instantiate(bom, transform.position, Quaternion.identity);
                timeStart = 0;
            }
        }
    }
}
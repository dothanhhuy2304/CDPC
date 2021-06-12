using UnityEngine;
using UnityEngine.UI;
using CDPC.GameManagers;
using CDPC.Enemy;
namespace CDPC.Player
{
    public class Weapon : MonoBehaviour
    {
        public static Weapon instance;
        [Header("Fox")]
        public GameObject projectile;
        public Transform shotPoint;
        [SerializeField] private float timeShots;
        [HideInInspector] private float startTimeShots = 1.5f;
        [Header("Ninja")]
        [SerializeField] private float radius = 1.5f;
        [SerializeField] private int minDamage = 10, maxDamage = 15;
        [SerializeField] private float timeSw;
        [HideInInspector] private float startTimeSw = 0.6f;
        [SerializeField] private float timeShots2;
        [SerializeField] private float startTimeShots2 = 1.5f;
        [Header("CoolDownSkill")]
        [SerializeField] private GameObject UIskill;
        private int choose;
        private Image coolDown1, coolDown2;
        private bool isCool1, isCool2, isColl3;
        [Tooltip("Singleton")]
        private PlayerAnimator playerAnimator;
        private SoundManger soundManger;

        private void Awake()
        {
            instance = this;
            UIskill = GameObject.Find("Skill2");
            choose = PlayerPrefs.GetInt("character");
            switch (choose)
            {
                case (int)Character.Fox:
                    UIskill.SetActive(false);
                    break;
                case (int)Character.Ninja:
                    UIskill.SetActive(true);
                    coolDown2 = GameObject.Find("CoolDown2").GetComponent<Image>();
                    break;
                default:
                    UIskill.SetActive(false);
                    break;
            }
            coolDown1 = GameObject.Find("CoolDown1").GetComponent<Image>();
            isCool1 = false;
            isCool2 = false;
            isColl3 = false;
            playerAnimator = PlayerAnimator.instance;
            soundManger = SoundManger.Instance;
        }

        private void Start()
        {
            timeShots = 0f;
            timeSw = 0f;
            timeShots2 = 0f;
        }

        void Update()
        {
            OnUpdateCoolDown();
        }


        void OnUpdateCoolDown()
        {
            switch (choose)
            {
                case (int)Character.Fox:
                    timeShots -= Time.deltaTime;
                    if (timeShots <= 0)
                    {
                        timeShots = 0;
                        isCool1 = false;
                        coolDown1.fillAmount = 0f;
                    }
                    else if (isCool1)
                    {
                        coolDown1.fillAmount += 1 / timeShots * Time.deltaTime;
                    }
                    break;
                case (int)Character.Ninja:
                    timeSw -= Time.deltaTime;
                    if (timeSw <= 0)
                    {
                        timeSw = 0;
                        coolDown1.fillAmount = 0f;
                    }
                    else if (isColl3)
                    {
                        coolDown1.fillAmount += 1 / timeSw * Time.deltaTime;
                    }

                    timeShots2 -= Time.deltaTime;
                    if (timeShots2 <= 0)
                    {
                        timeShots2 = 0;
                        isCool2 = false;
                        coolDown2.fillAmount = 0f;
                    }
                    else if (isCool2)
                    {
                        coolDown2.fillAmount += 1 / timeShots2 * Time.deltaTime;
                    }
                    break;
            }
        }

        public void FirstSkill()
        {
            switch (choose)
            {
                case (int)Character.Fox:
                    if (timeShots <= 0)
                    {
                        isCool1 = true;
                        GameObject projectiles = Instantiate(projectile, shotPoint.position, transform.rotation);
                        projectiles.name = "Projectile";
                        soundManger.audioSource.clip = soundManger.shot;
                        soundManger.audioSource.PlayOneShot(soundManger.shot);
                        playerAnimator.Attack();
                        timeShots = startTimeShots;
                    }
                    break;
                case (int)Character.Ninja:
                    Collider2D[] eneny = Physics2D.OverlapCircleAll(shotPoint.position, radius, 1 << LayerMask.NameToLayer("Enemy"));
                    if (timeSw <= 0)
                    {
                        isColl3 = true;
                        foreach (Collider2D hit in eneny)
                        {
                            if (hit)
                            {
                                hit.GetComponent<EnemyController>().TakeDamage(Random.Range(minDamage, maxDamage));
                            }
                        }
                        playerAnimator.Attack();
                        soundManger.audioSource.clip = soundManger.swords;
                        soundManger.audioSource.PlayOneShot(soundManger.swords);
                        timeSw = startTimeSw;
                    }
                    break;
            }
        }

        public void SecondSkill()
        {
            switch (choose)
            {
                case (int)Character.Fox:
                    UIskill.SetActive(false);
                    break;
                case (int)Character.Ninja:
                    if (timeShots2 <= 0)
                    {
                        isCool2 = true;
                        GameObject projectiles = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
                        projectiles.name = "Projectile";
                        soundManger.audioSource.clip = soundManger.shot;
                        soundManger.audioSource.PlayOneShot(soundManger.shot);
                        timeShots2 = startTimeShots2;
                    }
                    break;
                default:
                    UIskill.SetActive(false);
                    break;
            }
        }
    }
}
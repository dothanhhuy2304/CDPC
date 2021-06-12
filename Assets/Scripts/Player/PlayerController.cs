using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CDPC.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] public float speed;
        [SerializeField] public float jumpVelocity;
        [SerializeField] private Transform tagGround;
        [SerializeField] private Rigidbody2D rb;
        public bool dbjump = false;
        public bool isGrounded;
        [SerializeField] private float radiusGround = 0.2f;
        private Joystick variable;
        [SerializeField] private CapsuleCollider2D col;
        [Header("Dash")]
        [SerializeField] private float dashSpeed;
        Weapon weapon;
        [HideInInspector] private PlayerAnimator playerAnimator;
        EventTrigger attackOneEvent, attackTwoEvent, crouchEvent, dashEvent;
        bool isDash;
        float timeStart = 0f, resetTime = 5f;
        bool isCool;
        private UnityEngine.UI.Image coolDown;


        private void Awake()
        {
            col = FindObjectOfType<PlayerController>().GetComponent<CapsuleCollider2D>();
            weapon = FindObjectOfType<Weapon>().GetComponent<Weapon>();
            OnControl();
            //CollDown
            coolDown = GameObject.Find("CoolDown3").GetComponent<UnityEngine.UI.Image>();
        }
        private void OnControl()
        {
            attackOneEvent = GameObject.Find("Fire").GetComponent<EventTrigger>();
            attackTwoEvent = GameObject.Find("Skill2").GetComponent<EventTrigger>();
            crouchEvent = GameObject.Find("Crouch").GetComponent<EventTrigger>();
            dashEvent = GameObject.Find("Dash").GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            EventTrigger.Entry entry4 = new EventTrigger.Entry();
            EventTrigger.Entry entry5 = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry2.eventID = EventTriggerType.PointerDown;
            entry3.eventID = EventTriggerType.PointerDown;
            entry4.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener(data => { Attack(); });
            entry2.callback.AddListener(data => { AttackTwo(); });
            entry3.callback.AddListener(data => { Crouch(true); });
            entry4.callback.AddListener(data => { Crouch(false); });
            entry5.callback.AddListener(data => { Dash(variable.Horizontal); });
            attackOneEvent.triggers.Add(entry);
            attackTwoEvent.triggers.Add(entry2);
            crouchEvent.triggers.Add(entry3);
            crouchEvent.triggers.Add(entry4);
            dashEvent.triggers.Add(entry5);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            tagGround = GameObject.Find("tag_ground").transform;
            variable = FindObjectOfType<Joystick>().GetComponent<Joystick>();
            playerAnimator = PlayerAnimator.instance;
            isDash = false;
            isCool = false;
        }

        private void FixedUpdate()
        {
            OnMovement();
            OnCoolDown();
        }

        private void OnMovement()
        {
            isGrounded = Physics2D.OverlapCircle(tagGround.position, radiusGround, 1 << LayerMask.NameToLayer("ground"));
            playerAnimator.UpdateIsGrounded(isGrounded);
            if (!isDash)
            {
                Move(variable.Horizontal);
            }
            Jump();
        }

        private void OnCoolDown()
        {
            timeStart -= Time.deltaTime;
            if (timeStart <= 0f)
            {
                timeStart = 0;
            }
        }

        private void Move(float horizontalInput)
        {
            float xPosition = Mathf.Clamp(transform.position.x, -30f, 184f);
            transform.position = new Vector2(xPosition, transform.position.y);
            Vector2 moVel = rb.velocity;
            moVel.x = horizontalInput * speed;
            rb.velocity = moVel;
            playerAnimator.UpdateSpeed(horizontalInput);
        }

        private void Jump()
        {
            if (isGrounded && variable.Vertical > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                // dbjump = true;
            }
            //else if (dbjump)
            //{
            //    rb.velocity = Vector2.up * JumpVelocity;
            //    dbjump = false;
            //}
        }

        private void Attack()
        {
            weapon.FirstSkill();
        }
        private void AttackTwo()
        {
            weapon.SecondSkill();
        }

        private void Crouch(bool isCround)
        {
            col.enabled = !isCround;
            if (!isGrounded)
            {
                col.enabled = true;
            }
            playerAnimator.UpdateCrouch(isCround);
        }

        private void Dash(float horizontal)
        {
            if (timeStart == 0)
            {
                isCool = true;
                StartCoroutine(Dashing(horizontal));
                timeStart = resetTime;
                coolDown.fillAmount = 0f;
            }
            else if (isCool)
            {
                coolDown.fillAmount += 1 / timeStart * Time.deltaTime;
            }
        }
        IEnumerator Dashing(float horizontal)
        {
            isDash = true;
            if (isDash)
            {
                rb.AddForce(new Vector2(dashSpeed * horizontal, rb.velocity.y), ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.1f);
            rb.velocity = Vector3.zero;
            isDash = false;
        }
    }

    enum Character
    {
        Fox,
        Ninja
    }
}
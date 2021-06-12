using UnityEngine;

namespace CDPC.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        public static PlayerAnimator instance;

        [SerializeField] private Vector3 artScaleCache;
        [SerializeField] private Animator animator;
        const string Player_Run = "Speed";
        const string Player_Jump = "isGrounded";
        const string Player_Attack = "isAttack";
        const string Player_Crouch = "isCrouch";

        [Header("Effect")]
        [SerializeField] private ParticleSystem dust;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {

            animator = gameObject.GetComponent<Animator>();
        }

        void Flip(float currentSpeed)
        {
            artScaleCache = transform.localScale;
            if ((currentSpeed < 0 && artScaleCache.x > 0) || (currentSpeed > 0 && artScaleCache.x < 0))
            {
                artScaleCache.x *= -1;
                transform.localScale = artScaleCache;
                CreartDust();
            }

            //if (currentSpeed < 0 && isRight)
            //{
            //    transform.eulerAngles = new Vector3(0, -180, 0);
            //    isRight = false;
            //}
            //else if (currentSpeed > 0 && !isRight)
            //{
            //    transform.eulerAngles = Vector3.zero;
            //    isRight = true;
            //}
        }

        public void UpdateSpeed(float currentSpeed)
        {
            if (currentSpeed != 0)
            {
                CreartDust();
            }
            else
            {
                dust.Stop();
            }
            animator.SetFloat(Player_Run, currentSpeed);
            Flip(currentSpeed);
        }

        public void UpdateIsGrounded(bool isGrounded)
        {
            animator.SetBool(Player_Jump, isGrounded);
        }

        public void UpdateCrouch(bool isCrouching)
        {
            animator.SetBool(Player_Crouch, isCrouching);
        }

        public void Attack()
        {
            animator.SetTrigger(Player_Attack);
        }

        public void CreartDust()
        {
            dust.Play();
        }
    }
}
using UnityEngine;

namespace CDPC.Player
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public float fallGravity = 3f;
        public float upGravity = 2f;

        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;

            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (upGravity - 1) * Time.deltaTime;
            }
        }
    }
}

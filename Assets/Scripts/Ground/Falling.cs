using System.Collections;
using UnityEngine;

namespace CDPC.Mechanic
{
    public class Falling : MonoBehaviour
    {
        const string Player = "Player";
        const string Ground = "ground";
        [SerializeField]
        private Rigidbody2D rb;
        [Range(0.3f, 10f)]
        public float TimeDelay;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Player))
            {
                StartCoroutine(Fallings());
            }
            if (collision.collider.CompareTag(Ground))
            {
                Destroy(gameObject);
            }
        }

        IEnumerator Fallings()
        {
            yield return new WaitForSeconds(TimeDelay);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            yield return 0;
        }
    }
}
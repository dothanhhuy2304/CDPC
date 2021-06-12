using UnityEngine;

namespace CDPC.Player
{
    public class DestroyExplosion : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 0.9f);
        }
    }
}

using UnityEngine;

namespace CDPC.Mechanic
{
    public class DestroyFloatingText : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 1f);
        }
    }
}

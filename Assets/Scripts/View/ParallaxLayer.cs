using UnityEngine;

namespace CDPC.View
{
    public class ParallaxLayer : MonoBehaviour
    {
        [SerializeField] private float parallaxEffectMultiplier;

        private Camera cameraTransform;
        private Vector3 lastCameraPosition;

        private void Start()
        {
            cameraTransform = Camera.main;
            lastCameraPosition = cameraTransform.transform.position;
        }
        private void FixedUpdate()
        {
            Vector3 deltaMovement = cameraTransform.transform.position - lastCameraPosition;
            float parallaxEffectMultiplier = .5f;
            transform.position += deltaMovement * parallaxEffectMultiplier;
            lastCameraPosition = cameraTransform.transform.position;
        }
    }
}
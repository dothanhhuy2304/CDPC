using UnityEngine;
using CDPC.Player;

namespace CDPC.View
{
    public class CameraFlow : MonoBehaviour
    {
        private Transform playerController;
        [SerializeField] private int Smoothvalue = 2;
        [SerializeField] private float PosY = 1f;
        Camera cam;
        private void Start()
        {
            cam = Camera.main;
            playerController = FindObjectOfType<PlayerController>().transform;
        }


        void LateUpdate()
        {
            Vector3 Targetpos = new Vector3(playerController.transform.position.x, playerController.transform.position.y + PosY, cam.transform.position.z);
            transform.position = Vector3.Lerp(cam.transform.position, Targetpos, Time.deltaTime * Smoothvalue);
        }
    }
}
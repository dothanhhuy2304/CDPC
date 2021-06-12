using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDPC.Player;
using CDPC.GameManagers;

namespace CDPC.Mechanic
{
    public class GroundMoving : MonoBehaviour
    {
        public float speed = 0.05f;
        public float changeDirection = -1f;
        private Vector3 Move;
        [SerializeField] private Transform player;
        void Start()
        {
            player = FindObjectOfType<PlayerController>().transform;
            Move = transform.position;
        }

        void Update()
        {
            if (PauseGame.instance.isPause)
            {
                transform.position = transform.position;
            }
            if (!PauseGame.instance.isPause)
            {
                Move.x += speed;
                transform.position = Move;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ground"))
            {
                speed *= changeDirection;
            }
            if (collision.CompareTag("Player"))
            {
                player.transform.parent = transform;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                player.transform.parent = null;
            }
        }
    }
}
using UnityEngine;
using CDPC.GameManagers;

namespace CDPC.Player
{
    public class PlayerCheckPoint : MonoBehaviour
    {
        private PlayerController player;
        private GameManager gameManager;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
            gameManager = GameManager.instance;
        }
        private void Start()
        {
            if (gameManager.lastCheckPointPos == Vector2.zero)
            {
                transform.position = transform.position;
            }
            else
            {
                player.transform.position = gameManager.lastCheckPointPos;
            }
        }
    }
}
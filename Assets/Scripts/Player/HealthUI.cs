using UnityEngine;

namespace CDPC.Player
{
    public class HealthUI : MonoBehaviour
    {
        public Sprite[] heartSprite;
        public UnityEngine.UI.Image Heart;
        private HealthController healthController;
        private void Start()
        {
            healthController = HealthController.instance;
        }

        private void LateUpdate()
        {
            Heart.sprite = heartSprite[healthController.health];
        }
    }
}


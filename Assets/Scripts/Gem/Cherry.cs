using UnityEngine;
using CDPC.GameManagers;
using CDPC.Player;

namespace CDPC.Mechanic
{
    public class Cherry : MonoBehaviour
    {
        public float radius;
        private SoundManger soundManger;
        private HealthController healthController;

        private void Start()
        {
            soundManger = SoundManger.Instance;
            healthController = HealthController.instance;
        }

        private void Update()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
            foreach (var hit in hits)
            {
                if (hit)
                {
                    healthController.Heal(1);
                    soundManger.audioSource.clip = soundManger.destroy;
                    soundManger.audioSource.PlayOneShot(soundManger.destroy);
                    Destroy(gameObject);
                }
            }
        }
    }
}

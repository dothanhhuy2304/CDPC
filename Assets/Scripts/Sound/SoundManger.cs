using UnityEngine;

namespace CDPC.GameManagers
{
    public class SoundManger : MonoBehaviour
    {
        public static SoundManger Instance;

        public AudioSource audioSource;

        public AudioClip coins, swords, destroy, explosion, shot;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

    }
}
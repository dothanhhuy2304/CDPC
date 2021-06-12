using System.Collections;
using UnityEngine;
using CDPC.Player;
using CDPC.GameManagers;

public class Item : MonoBehaviour
{
    const string updateSpeed = "potion";
    private float currentSpeed;
    private SoundManger soundManger;
    public PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentSpeed = player.speed;
        soundManger = SoundManger.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(updateSpeed))
        {
            Destroy(collision.gameObject);
            soundManger.audioSource.clip = soundManger.coins;
            soundManger.audioSource.PlayOneShot(soundManger.coins);
            StartCoroutine(UpdateSpeed(5f));
        }
    }

    IEnumerator UpdateSpeed(float timeExist)
    {
        player.speed = 10f;
        yield return new WaitForSeconds(timeExist);
        player.speed = currentSpeed;
        yield return null;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    public GameObject loadingUI;
    public Slider slider;
    public Text txtProgress;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScreen(int index)
    {
        StartCoroutine(LoadScreens(index));
    }

    IEnumerator LoadScreens(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        loadingUI.SetActive(true);
        do
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            slider.value = progress;
            txtProgress.text = (asyncOperation.progress * 100 + "%").ToString();
            yield return null;
        }
        while (!asyncOperation.isDone);
    }
}

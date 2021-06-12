using UnityEngine;
using UnityEngine.UI;

namespace CDPC.GameManagers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;
        [SerializeField] private GameObject[] characters;
        [SerializeField] private Button btnPreview, btnNext;
        private int indexCharacter;
        private LoadingScreen loadingScreen;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].SetActive(i == 0);
            }
            loadingScreen = LoadingScreen.instance;
        }

        private void Update()
        {
            btnPreview.interactable = (indexCharacter != 0);
            btnNext.interactable = indexCharacter != characters.Length - 1;
        }

        public void ChangeCharacter(int index)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].SetActive(false);
            }
            indexCharacter = index;
            characters[index].SetActive(true);
        }


        public void ChangeCharacters(int index)
        {
            indexCharacter += index;
            ChangeCharacter(indexCharacter);
        }

        public void LoadScreen()
        {
            loadingScreen.LoadScreen(0);
            PlayerPrefs.SetInt("character", indexCharacter);
        }
    }
}
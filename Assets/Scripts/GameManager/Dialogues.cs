using UnityEngine;

[System.Serializable]
public class Dialogues : MonoBehaviour
{
    public string names = null;
    [TextArea(3, 10)]
    public string[] sentences;
}
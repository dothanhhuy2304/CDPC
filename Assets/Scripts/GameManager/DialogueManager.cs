using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Dialogues dialogues;
    public TMP_Text nameTxt;
    public TMP_Text dialogueTxt;
    public GameObject dialogueObject;
    public GameObject btnCountiue;

    private void Awake()
    {
        dialogues = FindObjectOfType<Dialogues>();
        dialogueObject.SetActive(true);
    }
    private void Start()
    {
        sentences = new Queue<string>();
        nameTxt.text = dialogues.names;
        //sentences.Clear();
        foreach (var sentence in dialogues.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDiaLogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentences)
    {
        dialogueTxt.text = "";
        foreach (char letter in sentences)
        {
            dialogueTxt.text += letter;
            btnCountiue.SetActive(false);
            yield return null;
        }
        btnCountiue.SetActive(true);
    }

    void EndDiaLogue()
    {
        dialogueObject.SetActive(false);
    }
}

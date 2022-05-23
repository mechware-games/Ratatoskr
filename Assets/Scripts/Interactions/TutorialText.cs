using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : Interactable
{
    public Queue<string> texts;
    public string[] messages;

    public TMP_Text tutTex;
    public GameObject canvas;

    private void Start()
    {
        texts = new Queue<string>();
        canvas.SetActive(false);

        foreach (string sentence in messages)
        {
            texts.Enqueue(sentence);
        }
    }

    public override void Action()
    {
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(texts.Count == 0)
        {
            EndText();
            canvas.SetActive(false);
            return;
        }

        canvas.SetActive(true);
        string text = texts.Dequeue();
        tutTex.text = text;
    }

    public void EndText()
    {
        texts.Clear();
        tutTex.text = "";
        canvas.SetActive(false);
        foreach (string sentence in messages)
        {
            texts.Enqueue(sentence);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            EndText();
        }
    }
}
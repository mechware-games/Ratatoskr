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
    public GameObject bgcanvas;

    public AudioSource sound;

    private bool active = true;

    private void Start()
    {
        texts = new Queue<string>();
        bgcanvas.SetActive(false);
        foreach (string sentence in messages)
        {
            texts.Enqueue(sentence);
        }
    }

    public override void Action()
    {
        DisplayNextSentence();
        if (active)
        {
            sound.Play();
            active = false;
        }
    }

    public void DisplayNextSentence()
    {
        if(texts.Count == 0)
        {
            EndText();
            bgcanvas.SetActive(false);
            return;
        }

        bgcanvas.SetActive(true);
        string text = texts.Dequeue();
        tutTex.text = text;
    }

    public void EndText()
    {
        texts.Clear();
        tutTex.text = "";
        bgcanvas.SetActive(false);
        foreach (string sentence in messages)
        {
            texts.Enqueue(sentence);
        }
        active = true;
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
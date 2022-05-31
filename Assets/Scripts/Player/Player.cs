using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public Vector3 LastCheckpoint;
    Renderer rend;

    private bool Dying = true;
    Animator anim;

    private GameObject fenrir;
    [SerializeField] private GameObject acornCanvas;

    private void Start()
    {
        anim = GetComponent(typeof(Animator)) as Animator;
        fenrir = GameObject.Find("Fenrir");
    }

    private void Update()
    {
        isFenrirAfterYou();
    }

    public void Death()
    {
        FenrirScript fenrir = GameObject.Find("Fenrir").GetComponent<FenrirScript>();
        fenrir.SetActive(false);
        fenrir.Despawn();
        transform.position = LastCheckpoint;
        gameOverScreen.Show();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        anim.Play("Death");

        if (Dying == true)
        {
            anim.SetBool("Dying", Dying);
        }
        else
        {
            anim.SetBool("Dying", Dying);
        }
    }

    public void isFenrirAfterYou()
    {
        if(fenrir.GetComponent<FenrirScript>().GetActive())
        {
            acornCanvas.SetActive(true);
        }
        else
        {
            acornCanvas.SetActive(false);
        }
    }
}
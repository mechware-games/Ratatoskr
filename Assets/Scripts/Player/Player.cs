using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public Vector3 LastCheckpoint;
    public GameObject self;
    Renderer rend;

    public void Death()
    {
        transform.position = LastCheckpoint;
        gameOverScreen.Show();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
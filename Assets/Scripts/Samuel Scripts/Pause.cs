using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

    public class Pause : MonoBehaviour
    {
        bool paused;
        public GameObject PauseMenu;
        public GameObject OptionsMenu;

        public GameObject FirstButtonOptions;
        
        public string LoadScene;


        public bool Paused { get => paused; set => paused = value; }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                SetPaused();
            }
        }
        public void SetPaused()
        {
            paused = !paused;
            if (paused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                OptionsMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    public void Options()
    {
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButtonOptions, null);
    }
}

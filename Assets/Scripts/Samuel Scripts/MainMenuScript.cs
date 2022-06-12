using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject MainMenu;
    public GameObject SkinStoreMenu;

    [SerializeField] private GameObject FirstButtonMain;
    [SerializeField] private GameObject FirstButtonOptions;
    [SerializeField] private GameObject FirstButtonStore;

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("PregameLobby");
    }

    public void Options()
    {
        OptionsMenu.SetActive(true);
        MainMenu.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButtonOptions, null);
    }

    public void SkinStore()
    {
        MainMenu.SetActive(false);
        SkinStoreMenu.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButtonStore, null);
    }

    public void BackSkinStore()
    {
        MainMenu.SetActive(true);
        SkinStoreMenu.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButtonMain, null);
    }
}
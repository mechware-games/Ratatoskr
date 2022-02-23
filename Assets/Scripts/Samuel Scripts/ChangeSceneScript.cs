using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
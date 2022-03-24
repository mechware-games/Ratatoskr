using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    [SerializeField]
    string nextscene;
    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextscene);
    }
}
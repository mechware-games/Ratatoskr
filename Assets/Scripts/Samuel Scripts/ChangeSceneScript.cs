using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    [SerializeField]
    string nextscene;
    public void NextScene()
    {
        SceneManager.LoadScene(nextscene);
    }
}
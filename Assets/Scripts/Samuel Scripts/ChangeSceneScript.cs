using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChangeSceneScript : MonoBehaviour
{
    [SerializeField]
    string nextscene;

    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextscene);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }
}
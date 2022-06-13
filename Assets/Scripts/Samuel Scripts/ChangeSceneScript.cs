using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChangeSceneScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;

    public void NextScene(int nextscene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextscene);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        Time.timeScale = 1;
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

        yield return new WaitForSeconds(10);
        Debug.Log("WAITED 2S");
        loadingScreen.SetActive(false);
    }
}
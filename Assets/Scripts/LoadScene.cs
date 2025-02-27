using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Image loadingBar;

    public void LoadSceneByIndex(int sceneIndex) => StartCoroutine(LoadSceneAsync(sceneIndex));

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingBar.fillAmount = 0f;
        loadingPanel.SetActive(true);
        while (!asyncOperation.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            yield return null;
        }
    }
}

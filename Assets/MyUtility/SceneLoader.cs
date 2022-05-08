using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(Coroutine(sceneIndex));
        // this.LoadSceneAsyncDelay(sceneIndex, 0);
    }

    IEnumerator Coroutine(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex);

    }
}

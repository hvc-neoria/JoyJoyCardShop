using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public static class SceneUtility
{
    //コルーチンのためにmonoを引数にしている
    public static void LoadSceneAsyncDelay(this MonoBehaviour mono, int sceneBuildIndex, float waitTime, Action onLoaded = default)
    {
        if (onLoaded != default)
        {
            SceneManager.sceneLoaded += (scene, loadSceneMode) => OnLoaded(onLoaded);
        }

        AsyncOperation a = SceneManager.LoadSceneAsync(sceneBuildIndex);
        a.allowSceneActivation = false;
        mono.Delay(waitTime, () => a.allowSceneActivation = true);
    }

    static void OnLoaded(Action onLoaded)
    {
        onLoaded.Invoke();
        SceneManager.sceneLoaded -= (scene, loadSceneMode) => OnLoaded(onLoaded);
    }

    public static void LoadSceneAsyncDelay(this MonoBehaviour mono, string sceneName, float waitTime)
    {
        AsyncOperation a = SceneManager.LoadSceneAsync(sceneName);
        a.allowSceneActivation = false;
        mono.Delay(waitTime, () => a.allowSceneActivation = true);
    }

    /// <summary>
    /// 最終シーンで呼び出すと0番のシーンを読み込む
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="waitTime"></param>
    public static void LoadNextSceneAsyncDelay(this MonoBehaviour mono, float waitTime)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int lastIndex = SceneManager.sceneCountInBuildSettings - 1;

        int index = currentIndex < lastIndex ? currentIndex + 1 : 0;
        LoadSceneAsyncDelay(mono, index, waitTime);
    }

    public static void ReloadSceneAsyncDelay(this MonoBehaviour mono, float waitTime)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        LoadSceneAsyncDelay(mono, currentIndex, waitTime);
    }

    public static void LoadSceneAsyncDelayAdditive(this MonoBehaviour mono, int sceneBuildIndex, float waitTime)
    {
        AsyncOperation a = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
        a.allowSceneActivation = false;
        mono.Delay(waitTime, () => a.allowSceneActivation = true);
    }

    public static void LoadSceneAsyncDelayAdditive(this MonoBehaviour mono, string sceneName, float waitTime)
    {
        AsyncOperation a = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        a.allowSceneActivation = false;
        mono.Delay(waitTime, () => a.allowSceneActivation = true);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineExtensionMethods
{
    /// <summary>
    /// CoroutineかDOTweenを使う。
    /// https://12px.com/blog/2016/11/unity-delay/
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="waitTime"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Coroutine Delay(this MonoBehaviour mono, float waitTime, Action action)
    {
        return mono.StartCoroutine(DelayCoroutine(waitTime, action));
    }

    static IEnumerator DelayCoroutine(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    public static Coroutine Loop(this MonoBehaviour mono, float time, float intervalTime, Action action)
    {
        return mono.StartCoroutine(LoopCoroutine(time, intervalTime, action));
    }

    static IEnumerator LoopCoroutine(float duration, float intervalTime, Action action)
    {
        float startedTime = Time.time;
        var waitForSeconds = new WaitForSeconds(intervalTime);

        while (Time.time < startedTime + duration)
        {
            action();
            yield return waitForSeconds;
        }
    }
}
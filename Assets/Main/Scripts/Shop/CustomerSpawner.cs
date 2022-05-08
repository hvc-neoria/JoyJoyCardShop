using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    Transform myTrans;
    Master.CustomerSpawner master;
    public static float waitTimeAvg { get; private set; }

    void Start()
    {
        myTrans = transform;
        master = Master.I.customerSpawner;
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        int maxCount = CurrentMaxCount(GotCounts.Total(Rarity.Rare), master.MaxCountProportionalConstant, min: master.MaxCountAtFirst, max: master.MaxCountAtLast);
        while (true)
        {
            if (myTrans.childCount < maxCount)
            {
                Instantiate(Resources.Load<GameObject>("Customer"),
                GetSpawnPos(), Quaternion.Euler(90f, 0, 0), myTrans);
                yield return new WaitForSeconds(WaitTime());
                continue;
            }
            yield return null;
        }
    }

    Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = myTrans.position;
        if (Random.Range(0, 2) == 0) spawnPos.x += -6f;
        return spawnPos;
    }

    /// <summary>
    /// y = ax^2 + minで表す二次関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="a">二次関数の比例定数</param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    int CurrentMaxCount(int x, float a, int min, int max)
    {
        float x2 = (float)(x * x);
        int r = Mathf.RoundToInt(x2 * a) + min;
        return Math.Min(max, r);
    }


    float WaitTime()
    {
        float waitTimeMax = -(float)GotCounts.Total(Rarity.Rare) * master.MaxWaitTimeDecrement + master.MaxWaitTimeAtFirst;
        waitTimeMax = Mathf.Max(waitTimeMax, master.MaxWaitTimeAtLast);
        float waitTime = UnityEngine.Random.Range(0.1f, waitTimeMax);
        return waitTime;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Master : ScriptableObject
{
    public static Master I
    {
        get
        {
            if (_instance is null)
            {
                _instance = Resources.Load<Master>(Path);
                if (_instance is null)
                {
                    Debug.LogError("Resourcesフォルダから" + Path + "が見つかりません。");
                }
            }
            return _instance;
        }
    }
    private static Master _instance;
    const string Path = "MasterData";


    [Header("Shop")]
    public CustomerSpawner customerSpawner;
    [Serializable]
    public class CustomerSpawner
    {
        [Range(1, 300)] public int MaxCountAtFirst = 5;
        [Range(1, 300)] public int MaxCountAtLast = 180;
        // y = ax^2 + bで表す二次関数の比例定数a
        [Range(0f, 10f)] public float MaxCountProportionalConstant = 0.8f;

        // 初回の最大待ち時間
        [Range(0.1f, 10f)] public float MaxWaitTimeAtFirst = 8f;
        // 最終的な最大待ち時間
        [Range(0.1f, 10f)] public float MaxWaitTimeAtLast = 0.2f;
        [Range(0.0f, 0.5f)] public float MaxWaitTimeDecrement = 0.5f;
    }
    [Range(1, 10)] public int packCountAtFirst = 3;
    [Range(1, 10)] public int packCountAtLast = 7;
    [Range(0.1f, 10f)] public float packCountIncrement = 0.16f;

    [Range(0, 100)] public int SalaryIncrement = 30;

    [Header("Home")]
    [Range(0.01f, 0.5f)] public float DownDuration = 0.3f;
    [Range(0.01f, 0.5f)] public float CardCollectionDownDuration = 0.3f;
    [Range(0.0f, 0.2f)] float CardDownWaitTime = 0.05f;
    WaitForSeconds _cardDownWaitForSeconds;
    public WaitForSeconds CardDownWaitForSeconds => _cardDownWaitForSeconds ??= new WaitForSeconds(CardDownWaitTime);
    [Range(2f, 4f)] public float CardZoomRate = 3.5f;
    [Range(0.01f, 0.3f)] public float CardZoomDuration = 0.15f;
    public float MarginX = 0.1f, MarginY = 0.11f;
}
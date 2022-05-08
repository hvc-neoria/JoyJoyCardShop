using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackInstantiater : MonoBehaviour
{
    Transform myTrans;

    void Awake()
    {
        myTrans = transform;
    }

    public GameObject Instantiate(float offsetX, int index)
    {
        float x = offsetX + EtcUtility.NormalDistribution() / 3f * 0.01f;
        Vector3 pos = myTrans.position;
        return Instantiate(
            Resources.Load<GameObject>("ReadPack"),
            new Vector3(x, 1.001f + (float)index * 0.002f, pos.z),
            Quaternion.Euler(90f, 0, 0)
        );
    }
}
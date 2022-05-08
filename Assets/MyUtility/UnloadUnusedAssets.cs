using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadUnusedAssets : MonoBehaviour
{
    void Start()
    {
        this.Loop(Mathf.Infinity, 60f * 5f, () => Resources.UnloadUnusedAssets());
    }

    [ContextMenu("Unload")]
    void Unload()
    {
        Debug.Log("Unload");
        Resources.UnloadUnusedAssets();
    }
}

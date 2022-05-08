using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundReplacer : MonoBehaviour
{
    // 多数配置されるプレハブについては、この変数を参照して別のプレハブに置き換える
    public static bool Enabled { get; private set; }
    [SerializeField] ReplaceCombination[] replaceCombinations;

    // 多数配置しないゲームオブジェクトに反映
    [System.Serializable]
    class ReplaceCombination
    {
        [SerializeField] AudioSource target;
        [SerializeField] AudioClip audioClip;
        [SerializeField] float volume = 1f;

        public void Replace()
        {
            target.clip = audioClip;
            target.volume = volume;
        }
    }

    void Start()
    {
        if (Enabled) ReplaceAll();
    }

    public void Enable()
    {
        Enabled = true;
        ReplaceAll();
    }

    void ReplaceAll()
    {
        foreach (var item in replaceCombinations)
        {
            item.Replace();
        }
    }
}

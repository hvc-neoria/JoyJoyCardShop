using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    AudioSource source;
    [SerializeField] Music[] musics;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(int index)
    {
        source.clip = musics[index].audioClip;
        source.volume = musics[index].volume;
        source.Play();
    }

    [System.Serializable]
    class Music
    {
        public AudioClip audioClip;
        public float volume;
    }
}

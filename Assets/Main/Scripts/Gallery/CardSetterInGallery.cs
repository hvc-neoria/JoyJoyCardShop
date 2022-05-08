using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rarity;

/// <summary>
/// カードのテクスチャ設定やアナザーモード専用ズーム音の設定を行う
/// </summary>
public class CardSetterInGallery : MonoBehaviour
{
    [SerializeField] AudioClip anotherZoomSE;
    [SerializeField] AudioClip anotherUnzoomSE;
    [SerializeField] float volume = 1f;

    void Awake()
    {
        StartCoroutine(Coroutine());
    }

    // SetTexture（テクスチャ設定）中にTryReplaceSoundを実行すると
    // WebGLでテクスチャ設定に失敗することがあるため、
    // TryReplaceSoundが完了してからTrySetTextureToCardsを実行する
    IEnumerator Coroutine()
    {
        yield return StartCoroutine(TryReplaceSound());
        yield return null;
        yield return StartCoroutine(TrySetTextureToCards());
    }

    IEnumerator TryReplaceSound()
    {
        if (SoundReplacer.Enabled)
        {
            var zoomers = GetComponentsInChildren<CardZoomer>();
            foreach (var zoomer in zoomers)
            {
                zoomer.zoomSE = anotherZoomSE;
                zoomer.unzoomSE = anotherUnzoomSE;
                zoomer.volume = volume;
            }
        }
        yield break;
    }

    // 一度に全てのテクスチャを設定すると
    // 謎の黒い表示になるので、
    // それを回避するためにコルーチンを使用する
    IEnumerator TrySetTextureToCards()
    {
        Card[] cards = GetComponentsInChildren<Card>();
        int n = 0;

        for (int i = 0; i < GotCounts.Length(Common); i++)
        {
            if (GotCounts.Get(Common, i) > 0)
            {
                cards[n].SetTexture(Common, i + 1);
                yield return null;
            }
            n++;
        }
        for (int i = 0; i < GotCounts.Length(Rare); i++)
        {
            if (GotCounts.Get(Rare, i) > 0)
            {
                cards[n].SetTexture(Rare, i + 1);
                yield return null;
            }
            n++;
        }
        for (int i = 0; i < GotCounts.Length(SR); i++)
        {
            if (GotCounts.Get(SR, i) > 0)
            {
                cards[n].SetTexture(SR, i + 1);
                yield return null;
            }
            n++;
        }
        for (int i = 0; i < GotCounts.Length(UR); i++)
        {
            if (GotCounts.Get(UR, i) > 0)
            {
                cards[n].SetTexture(UR, i + 1);
                yield return null;
            }
            n++;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PackButton : MonoBehaviour
{
    readonly float ScaleTime = 0.1f;
    readonly Money Price = new Money(150);
    const int CardNumInLine = 5;
    const string CardPrefabName = "Card";
    const string AnotherCardPrefabName = "CardAnotherModeVariant";

    Transform myTrans;
    Transform cardCollectionTrans;
    CardCollection cardCollectionScript;
    PlayersMoney playersMoney;
    Vector3 defaultScale;

    Coroutine buyCoroutine;
    Coroutine waitCoroutine;

    void Start()
    {
        myTrans = transform;
        playersMoney = FindObjectOfType<PlayersMoney>();
        cardCollectionTrans = GameObject.Find("CardCollection").transform;
        cardCollectionScript = cardCollectionTrans.GetComponent<CardCollection>();
        defaultScale = myTrans.localScale;

        DOTween.Sequence().SetDelay(0.3f)
            .Append(myTrans.DOShakeRotation(1f, Vector3.forward * 10f))
            .AppendInterval(2f)
            .SetLoops(-1)
            .Play();
    }

    // TODO:非常に早くパックを連打すると、勝手に購入し続けてしまう不具合が発生している
    // 高速連打するとStopBuyがなぜか二度実行される。しかし、購入が止まらない。
    // 原因調査の参考 https://qiita.com/yuji_yasuhara/items/6f50ecdd5d59e83aac99
    // コルーチンの問題か？
    public void StartBuy() => buyCoroutine = StartCoroutine(BuyCoroutine());
    public void StopBuy()
    {
        Debug.Log($"stop {buyCoroutine}");
        StopCoroutine(buyCoroutine);
        buyCoroutine = null;
        Debug.Log($"stop after {buyCoroutine}");
    }

    IEnumerator BuyCoroutine()
    {
        Debug.Log($"buy");
        while (true)
        {
            TryBuy();
            yield return null;
        }
    }

    void TryBuy()
    {
        if (waitCoroutine != null) return;
        if (Card.IsDrawnUR) return;

        if (playersMoney.TrySub(Price))
        {
            // カード排出連射バグ発生中にズームして、
            // 画面遷移してしまった際にZoomerがずっと存在し続けてしまう
            // バグの暫定対応
            if (CardZoomer.CurrentCardZoomer != null)
            {
                CardZoomer.CurrentCardZoomer.TryUnZoom();
            }

            cardCollectionScript.Down();
            List<Card> cards = CreateCards();
            waitCoroutine = StartCoroutine(DownInOrder(cards));
            myTrans.DOScale(defaultScale * 0.9f, ScaleTime / 2f).SetLoops(2, LoopType.Yoyo);
        }
    }

    /// <summary>
    /// 順番にカードを下げる
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    IEnumerator DownInOrder(List<Card> cards)
    {
        for (int i = 0; i < CardNumInLine; i++)
        {
            cards[i].DownAtFirst(i);
            yield return Master.I.CardDownWaitForSeconds;
        }
        waitCoroutine = null;
    }

    private List<Card> CreateCards()
    {
        List<Card> cards = new List<Card>(CardNumInLine);
        for (int i = 0; i < CardNumInLine; i++)
        {
            string prefabName = SoundReplacer.Enabled ? AnotherCardPrefabName : CardPrefabName;
            GameObject cardGameObject = Instantiate(Resources.Load(prefabName),
                myTrans.position - Vector3.forward * 0.1f, Quaternion.identity, cardCollectionTrans) as GameObject;
            cardCollectionScript.Add(cardGameObject.transform);

            var card = cardGameObject.GetComponent<Card>();
            card.SetTextureRandom();
            card.SetRotY90();
            cards.Add(card);
        }
        return cards;
    }
}

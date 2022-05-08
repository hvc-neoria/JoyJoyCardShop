using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Rarity;

public class Card : MonoBehaviour
{
    public static Money UnitSalary => new Money((GotCounts.Total(SR) + 1) * Master.I.SalaryIncrement);
    public static bool IsDrawnUR { get; set; }

    const string TexturePassPrefix = "JoyJoyCard/JoyJoyCard_";
    [SerializeField] DownSe[] downSes = new DownSe[4];
    [System.Serializable]
    class DownSe
    {
        [SerializeField] public AudioClip audioClip;
        [SerializeField] public float volume = 1f;
    }

    [SerializeField] bool isURForDebug; //デバッグ用
    Transform myTrans;
    Rarity rarity;

    public static event Action OnDrawnUR = delegate { };

    CardZoomer zoomer;

    void Awake()
    {
        myTrans = transform;
        zoomer = GetComponent<CardZoomer>();
    }

    // レア度決定、カード種決定、入手数増加、テクスチャ設定などが混在している
    // カードの種類を決定する
    public void SetTextureRandom()
    {
        rarity = isURForDebug ? UR : GotCounts.GetRarityRandom();
        int index = GotCounts.GetIndexRandom(rarity);

        TryCreateNewBadge(rarity, index);
        GotCounts.Increment(rarity, index);

        if (rarity == UR && !IsDrawnUR)
        {
            IsDrawnUR = true;
            GetComponent<BoxCollider>().enabled = false;
        }

        // 1足している理由は、テクスチャのファイル名を1始まりとしているため
        SetTexture(rarity, index + 1);
    }

    void TryCreateNewBadge(Rarity rarity, int number)
    {
        int gotCount = GotCounts.Get(rarity, number);
        if (gotCount == 0)
        {
            var newLabel = Instantiate(
                Resources.Load<GameObject>("NewBadge"),
                myTrans.position + new Vector3(-0.0335f, 0.05f, -0.01f),
                Quaternion.identity,
                myTrans
            );
            newLabel.transform.localScale = new Vector3(0.1429f, 0.1f, 0.1f);
        }
    }

    public void SetTexture(Rarity rarity, int number)
    {
        Debug.Log("SetTexture" + number);
        string pass = TexturePassPrefix + GotCounts.ToRarityString(rarity) + number.ToString("D2");
        Texture texture = Resources.Load<Texture>(pass);
        GetComponent<Renderer>().SetTexture(texture);
    }

    public void SetRotY90()
    {
        transform.eulerAngles = Vector3.up * 90f;
    }

    /// <summary>
    /// カードが生成されて最初の下がるモーション
    /// </summary>
    /// <param name="index"></param>
    public void DownAtFirst(int index)
    {
        Vector3 destination = new Vector3((float)index * Master.I.MarginX - 0.2f, -Master.I.MarginY - 0.02f, 0.1f);
        myTrans.DOBlendableMoveBy(destination, Master.I.DownDuration)
            .OnComplete(OnComplete);

        myTrans.DORotate(Vector3.zero, Master.I.DownDuration);

        // サウンド関連の演出
        AudioSource cameraAudioSource = Camera.main.GetComponent<AudioSource>();
        if (rarity == UR) cameraAudioSource.Stop();
        cameraAudioSource.PlayOneShot(downSes[(int)rarity].audioClip, downSes[(int)rarity].volume);
    }

    void OnComplete()
    {
        zoomer.canZoom = true;
        if (rarity == UR) StartUREvent();
    }

    void StartUREvent()
    {
        this.Delay(1f, () =>
        {
            OnDrawnUR.Invoke();
            zoomer.ToggleZoom();
            Camera.main.GetComponent<AudioSource>().volume = 0.45f;
            Camera.main.GetComponent<AudioSourceController>().PlayClip(0);
            DispClearUIGroup();
        });
    }

    static void DispClearUIGroup()
    {
        var clearUIGroup = GameObject.Find("GameClearUIGroup").GetComponent<CanvasGroup>();
        clearUIGroup.blocksRaycasts = true;
        clearUIGroup.alpha = 1f;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CardZoomer : MonoBehaviour
{
    [SerializeField] public AudioClip zoomSE, unzoomSE;
    public float volume = 4f;

    public static CardZoomer CurrentCardZoomer { get; private set; }
    static Vector3 SavedPos;

    [NonSerialized] public bool canZoom;
    Transform myTrans;
    Vector3 defaultScale;
    Tweener moveTweener;

    void Awake()
    {
        myTrans = transform;
        defaultScale = myTrans.localScale;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            canZoom = true;
        }
    }

    public void ToggleZoom()
    {
        if (CurrentCardZoomer is null)
        {
            TryZoom();
        }
        else if (CurrentCardZoomer == this)
        {
            TryUnZoom();
        }
    }

    void TryZoom()
    {
        if (!canZoom) return;

        moveTweener.Complete();
        CurrentCardZoomer = this;

        GoToScreenCenter();
        myTrans.DOScale(defaultScale * Master.I.CardZoomRate, Master.I.CardZoomDuration);

        Camera.main.GetComponent<AudioSource>().PlayOneShot(zoomSE, volume);
        GetComponent<BoxCollider>().size = Vector3.one * 4f;

        TryHideNewBadge();
    }

    void TryHideNewBadge()
    {
        if (myTrans.childCount == 1)
        {
            var child = myTrans.GetChild(0);
            child.GetComponent<Renderer>().enabled = false;
        }
    }

    void GoToScreenCenter()
    {
        SavedPos = myTrans.position;

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 screenCenter = new Vector3(cameraPos.x, cameraPos.y, -0.6f);
        moveTweener = myTrans.DOMove(screenCenter, Master.I.CardZoomDuration);
    }



    public void TryUnZoom()
    {
        if (!canZoom) return;

        CurrentCardZoomer = null;

        moveTweener = myTrans.DOMove(SavedPos, Master.I.CardZoomDuration);
        myTrans.DOScale(defaultScale, Master.I.CardZoomDuration);

        Camera.main.GetComponent<AudioSource>().PlayOneShot(unzoomSE, volume);
        GetComponent<BoxCollider>().size = Vector3.one;
    }

    /// <summary>
    /// ゲームクリア時のCloseButtonが実行する
    /// </summary>
    public static void ToggleZoomStatic()
    {
        CurrentCardZoomer.GetComponent<BoxCollider>().enabled = true;
        CurrentCardZoomer.ToggleZoom();
    }
}

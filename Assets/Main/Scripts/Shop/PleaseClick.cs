using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PleaseClick : MonoBehaviour
{
    static bool isFirst = true;

    void Awake()
    {
        if (isFirst)
        {
            FindObjectOfType<CustomerSpawner>().enabled = false;
            FindObjectOfType<BarcodeReader>().enabled = false;
            Camera.main.GetComponent<AudioSource>().Stop();
        }
        else
        {
            Hide();
        }
    }

    public void OnClick()
    {
        isFirst = false;
        FindObjectOfType<CustomerSpawner>().enabled = true;
        FindObjectOfType<BarcodeReader>().enabled = true;
        Camera.main.GetComponent<AudioSource>().Play();
        Hide();
    }

    void Hide()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
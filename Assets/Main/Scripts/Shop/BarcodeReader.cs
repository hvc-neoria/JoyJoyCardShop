using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcodeReader : MonoBehaviour
{
    Transform myTrans;
    AudioSource se;
    MeshRenderer flashRenderer;

    void Start()
    {
        se = GetComponent<AudioSource>();
        myTrans = transform;
        flashRenderer = myTrans.Find("Flash").GetComponent<MeshRenderer>();
    }

    void Update()
    {
        RaycastHit hitInfo = EtcUtility.MousePositionToHitInfo();
        bool isNotHit = hitInfo.collider == null;
        if (isNotHit) return;

        myTrans.position = hitInfo.point;

        if (hitInfo.collider.CompareTag("Barcode"))
        {
            var card = hitInfo.collider.GetComponentInParent<ReadPack>();
            if (!card.IsRead)
            {
                card.EnableReadFlag();
                se.PlayOneShot(se.clip);
                Flash();
            }
        }
    }

    void Flash()
    {
        flashRenderer.enabled = true;
        this.Delay(0.1f, () => flashRenderer.enabled = false);
    }
}

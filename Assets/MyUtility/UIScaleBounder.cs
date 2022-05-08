using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIScaleBounder : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(Vector3.one * 0.1f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo);
    }
}

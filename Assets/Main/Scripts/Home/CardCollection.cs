using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardCollection : MonoBehaviour
{
    List<Transform> transes = new List<Transform>(32);

    public void Add(Transform trans)
    {
        transes.Add(trans);
    }

    public void Down()
    {
        foreach (var trans in transes)
        {
            trans.DOBlendableMoveBy(Vector3.up * -Master.I.MarginY, Master.I.CardCollectionDownDuration);
        }
    }
}

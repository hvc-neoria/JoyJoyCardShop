using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CameraMoverInCardCollection : MonoBehaviour
{
    const float TopLimit = 0f;
    Transform myTrans;
    Transform bottomCardTrans;
    Tweener tweener;

    void Awake()
    {
        myTrans = transform;
    }

    void Update()
    {
        ReturnToAvailableArea();
        if (CardZoomer.CurrentCardZoomer != null) return;

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput != 0)
        {
            myTrans.DOBlendableMoveBy(Vector3.up * wheelInput, 0.2f);
            if (bottomCardTrans == null) TryGetBottomCardTrans();
        }
    }

    void ReturnToAvailableArea()
    {
        if (myTrans.position.y > TopLimit)
        {
            if (tweener == null || (tweener != null && !tweener.IsComplete()))
            {
                tweener = myTrans.DOMoveY(TopLimit, 0.1f);
            }
            return;
        }

        float currentBottom = bottomCardTrans?.position.y ?? TopLimit;
        if (myTrans.position.y < currentBottom)
        {
            tweener = myTrans.DOMoveY(currentBottom, 0.1f);
        }
    }

    void TryGetBottomCardTrans()
    {
        Transform[] cardTranses = GameObject.Find("CardCollection").GetComponentsInChildrenWithoutSelf<Transform>();
        if (cardTranses.Length == 0) return;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bottomCardTrans = cardTranses[cardTranses.Length - 1];
            return;
        }

        bottomCardTrans = cardTranses[0];
    }
}

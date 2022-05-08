using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ReadPack : MonoBehaviour
{
    const float MoveDuration = 0.5f;
    const float MoneyAddingDelay = 0.3f;
    public bool IsRead { get; private set; }

    void Start()
    {
        GoForward();
    }

    void GoForward()
    {
        float zDistance = Random.Range(0.20f, 0.15f);
        transform.DOMoveZ(-zDistance, MoveDuration);
    }

    public void EnableReadFlag()
    {
        IsRead = true;
        PlayersMoney playersMoney = GameObject.FindWithTag("PlayersMoney").GetComponent<PlayersMoney>();
        this.Delay(MoneyAddingDelay, () => playersMoney.Add(Card.UnitSalary));
        GoBack();
    }

    void GoBack()
    {
        transform.DOMoveZ(0.5f, MoveDuration).OnComplete(() => Destroy(gameObject));
    }
}

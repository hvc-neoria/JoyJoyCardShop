using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayersMoney : MonoBehaviour
{
    [SerializeField] int InitMoney;

    Text text;
    // DOTweenで数値アニメーションするために使用
    int viewValue;

    static private Money _money;
    public Money Money
    {
        get { return _money; }
        set
        {
            viewValue = value.value;
            text.text = viewValue.ToString("N0") + "円";
            _money = value;
        }
    }
    public static Money Income { get; private set; } = new Money(0);
    public static Money Expense { get; private set; } = new Money(0);

    AudioSource audioSource;

    void Awake()
    {
        _money ??= new Money(InitMoney);
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        Money = _money;
    }

    public void Add(Money money)
    {
        _money = Money.Plus(money);

        audioSource.time = 0.1f;
        float time = (float)(_money.value - viewValue) / 1000f;

        this.Loop(time, 0.05f, () => audioSource.Play());
        DOTween.To(() => viewValue, x => viewValue = x, _money.value, time)
        .OnUpdate(() => text.text = viewValue.ToString("N0") + "円");
        Income = Income.Plus(money);
    }

    public bool TrySub(Money money)
    {
        var result = Money.Minus(money);
        if (result.IsMoreThan0())
        {
            Money = result;
            Expense = Expense.Plus(money);
            return true;
        }
        else
        {
            return false;
        }
    }
}
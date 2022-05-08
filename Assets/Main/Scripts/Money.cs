using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    const int min = -1000000000;
    const int max = 1000000000;

    public readonly int value;

    public Money(int value){
        this.value = Mathf.Clamp(value, min, max);
    }

    public Money Plus(Money money)
    {
        int result = this.value + money.value;
        return new Money(result);
    }

    public Money Minus(Money money)
    {
        int result = this.value - money.value;
        return new Money(result);
    }

    public bool IsMoreThan0()
    {
        return this.value >= 0;
    }
}
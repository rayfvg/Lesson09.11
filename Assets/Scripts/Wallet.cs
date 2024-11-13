using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet 
{
    public event Action<int> Changed;

    private Dictionary<Wallet, int> _wallets = new Dictionary<Wallet, int>();
    public Wallet(int maxValue)
    {
        if (maxValue < 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        MaxValue = maxValue;
    }

    public int MaxValue { get; }
    public int Value { get; private set; }

    public bool IsEnoughSpace(int value) => Value + value <= MaxValue;

    public bool IsEnoughMoney(int value) => Value - value >= 0;

    public void Add(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Не может быть отрицательным");

        if (IsEnoughSpace(value) == false)
            return;

        Value += value;

        Changed?.Invoke(Value);
    }

    public void Remove(int value)
    {
        if(value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Не может быть отрицательным");

        if (IsEnoughMoney(value) == false)
            return;

        Value -= value;

        Changed?.Invoke(Value);
    }
}
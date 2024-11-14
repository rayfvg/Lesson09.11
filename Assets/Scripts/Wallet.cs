using System;
using System.Collections.Generic;

public class Wallet 
{
    public event Action<CurrencyType, int> Changed;

    private Dictionary<CurrencyType, int> _currencies;
    public Wallet(int maxValue)
    {
        if (maxValue < 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        MaxValue = maxValue;

        _currencies = new Dictionary<CurrencyType, int>
    {
        { CurrencyType.Money, 0 },
        { CurrencyType.Diamond, 0 },
        { CurrencyType.Energy, 0 }
    };

    }

    public int MaxValue { get; }
    public int Value { get; private set; }

    public bool IsEnoughSpace(CurrencyType type, int value) => GetCurrencyValue(type) + value <= MaxValue;

    public bool IsEnoughMoney(CurrencyType type, int value) => GetCurrencyValue(type) - value >= 0;

    public int GetCurrencyValue(CurrencyType type)
    {
        int value;
        if (_currencies.TryGetValue(type, out value))
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    public void Add(CurrencyType type, int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Не может быть отрицательным");

        if (IsEnoughSpace(type, value) == false)
            return;

        _currencies[type] += value;

        Changed?.Invoke(type, _currencies[type]);
    }

    public void Remove(CurrencyType type, int value)
    {
        if(value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Не может быть отрицательным");

        if (IsEnoughMoney(type, value) == false)
            return;

        _currencies[type] -= value;

        Changed?.Invoke(type, _currencies[type]);
    }
}
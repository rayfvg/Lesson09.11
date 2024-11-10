using System;
using TMPro;
using UnityEngine;

public class WalletExample : MonoBehaviour
{
    [SerializeField] private int _maxValueMoney;
    [SerializeField] private TMP_Text _countMoneyText;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = new Wallet(_maxValueMoney);

        _wallet.Changed += OnWalletChanged;
        _countMoneyText.text = _wallet.Value.ToString();
    }

    private void OnDestroy()
    {
        _wallet.Changed -= OnWalletChanged;
    }

    public void TryAdd(int value)
    {
        if (_wallet.IsEnoughSpace(value))
        {
            _wallet.Add(value);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Нет места");
        }
    }

    public void TryRemove(int value)
    {
        if (_wallet.IsEnoughMoney(value))
        {
            _wallet.Remove(value);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Нет столько денег");
        }
    }

    private void OnWalletChanged(int newValue)
    {
        _countMoneyText.text = newValue.ToString();
    }
}
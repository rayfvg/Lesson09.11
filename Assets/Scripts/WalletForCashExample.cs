using System;
using TMPro;
using UnityEngine;

public class WalletForCashExample : MonoBehaviour
{
    [SerializeField] private int _maxValueMoney;

    [SerializeField] private TMP_Text _countMoneyText;

    private Wallet _walletMoney;


    private void Awake()
    {
        _walletMoney = new Wallet(_maxValueMoney);

        _walletMoney.Changed += OnWalletChanged;

        _countMoneyText.text = _walletMoney.Value.ToString();
    }

    private void OnDestroy()
    {
        _walletMoney.Changed -= OnWalletChanged;
    }

    public void TryAdd(int value)
    {
        if (_walletMoney.IsEnoughSpace(value))
        {
            _walletMoney.Add(value);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Нет места");
        }
    }

    public void TryRemove(int value)
    {
        if (_walletMoney.IsEnoughMoney(value))
        {
            _walletMoney.Remove(value);
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
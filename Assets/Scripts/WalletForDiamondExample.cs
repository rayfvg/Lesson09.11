using System;
using TMPro;
using UnityEngine;

public class WalletForDiamondExample : MonoBehaviour
{
    [SerializeField] private int _maxValueDiamond;

    [SerializeField] private TMP_Text _countDiamondText;

    private Wallet _walletMoney;


    private void Awake()
    {
        _walletMoney = new Wallet(_maxValueDiamond);

        _walletMoney.Changed += OnWalletChanged;

        _countDiamondText.text = _walletMoney.Value.ToString();
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
            throw new ArgumentOutOfRangeException(nameof(value), "Нет столько алмазов");
        }
    }

    private void OnWalletChanged(int newValue)
    {
        _countDiamondText.text = newValue.ToString();
    }
}

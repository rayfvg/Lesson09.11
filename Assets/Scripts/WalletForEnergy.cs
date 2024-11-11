using System;
using TMPro;
using UnityEngine;

public class WalletForEnergy : MonoBehaviour
{
    [SerializeField] private int _maxValueEnergy;

    [SerializeField] private TMP_Text _countEnergyText;

    private Wallet _walletMoney;


    private void Awake()
    {
        _walletMoney = new Wallet(_maxValueEnergy);

        _walletMoney.Changed += OnWalletChanged;

        _countEnergyText.text = _walletMoney.Value.ToString();
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
            throw new ArgumentOutOfRangeException(nameof(value), "Нет столько энергии");
        }
    }

    private void OnWalletChanged(int newValue)
    {
        _countEnergyText.text = newValue.ToString();
    }
}
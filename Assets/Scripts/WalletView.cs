using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private List<CurrencyView> currencyViews;

    private Dictionary<CurrencyType, CurrencyView> _currencyViewDict;
    private Wallet _wallet;

    public void Initialize(Wallet wallet, Dictionary<CurrencyType, Sprite> currencyIcons)
    {
        _wallet = wallet;
        _currencyViewDict = new Dictionary<CurrencyType, CurrencyView>();

        for (int i = 0; i < currencyViews.Count; i++)
        {
            CurrencyType type = (CurrencyType)i;
            currencyViews[i].SetCurrency(currencyIcons[type], wallet.GetCurrencyValue(type));
            _currencyViewDict[type] = currencyViews[i];
        }

        _wallet.Changed += OnWalletChanged;
    }

    private void OnWalletChanged(CurrencyType type, int newValue)
    {
        if (_currencyViewDict.TryGetValue(type, out CurrencyView view))
        {
            view.ValueText.text = newValue.ToString();
        }
    }
}

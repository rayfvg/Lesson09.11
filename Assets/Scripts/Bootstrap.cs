using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public Sprite MoneyIcon;
    public Sprite DiamondIcon;
    public Sprite EnergyIcon;

    [SerializeField] private WalletView _walletView;

    [SerializeField] private int _timeMax;
    [SerializeField] private MonoBehaviour _contex;
    [SerializeField] private ElementsTimerView _elementsTimerView;
    [SerializeField] private TimeSliderView _timeSliderView;

    private Dictionary<CurrencyType, Sprite> _currencyIcons = new Dictionary<CurrencyType, Sprite>();

    private Timer _timer;

    private void Awake()
    {
        Wallet wallet = new Wallet(100);
        _timer = new Timer(_timeMax, _contex);

        _currencyIcons.Add(CurrencyType.Money, MoneyIcon);
        _currencyIcons.Add(CurrencyType.Diamond, DiamondIcon);
        _currencyIcons.Add(CurrencyType.Energy, EnergyIcon);

        _walletView.Initialize(wallet, _currencyIcons);
        _elementsTimerView.Initialize(_timer);
        _timeSliderView.Initialize(_timer);

        wallet.Add(CurrencyType.Money, 10);
        wallet.Add(CurrencyType.Energy, 40);
    }
}
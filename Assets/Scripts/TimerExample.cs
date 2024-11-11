using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerExample : MonoBehaviour
{
    [SerializeField] private float _timeMax;
    [SerializeField] private WalletForCashExample _contex;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _hearåPrefab;

    [SerializeField] private GameObject _conteinerHeart;

    [SerializeField] private List<GameObject> _hearts = new List<GameObject>();
    private Timer _timerExsample;


    private void Awake()
    {
        _timerExsample = new Timer(_timeMax, _contex);
        _timerText.text = _timerExsample.CurrentTimer.ToString();
        _timerExsample.OnTick += OnTimerChancet;

        for (int i = 0; i < _timeMax; i++)
        {
            GameObject heard = Instantiate(_hearåPrefab, transform.position, Quaternion.identity, _conteinerHeart.transform);
            _hearts.Add(heard);
        }
    }

    private void OnDestroy()
    {
        _timerExsample.OnTick -= OnTimerChancet;
    }

    private void OnTimerChancet(float time)
    {
        _timerText.text = time.ToString();
        _slider.value = (time / _timeMax);

        for (int i = (int)_timeMax; i > 0; i--)
            _hearts[(int)time].gameObject.SetActive(false);
    }

    public void StartTimer() => _timerExsample.Start();

    public void StopTimer() => _timerExsample.Stop();

    public void ResetT()
    {
        _timerExsample.ResetT();
        _slider.value = _timerExsample.CurrentTimer / _timeMax;
        _timerText.text = _timerExsample.CurrentTimer.ToString();

        foreach (GameObject heard in _hearts)
            heard.gameObject.SetActive(true);
    }

    public void Pause() => _timerExsample.Pause();
}
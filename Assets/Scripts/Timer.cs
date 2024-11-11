using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<float> OnTick;

    private float _timeMaximum;
    private float _progress;
    private MonoBehaviour _contex;

    private bool _isWorking = false;

    public Timer(float timeMaximum, MonoBehaviour contex)
    {
        _timeMaximum = timeMaximum;
        _contex = contex;

        if (_timeMaximum < 0)
            throw new ArgumentOutOfRangeException(nameof(timeMaximum), "Не может быть меньше нуля");

        CurrentTimer = _timeMaximum;
        _progress = _timeMaximum;
        _contex = contex;
    }

    public float CurrentTimer { get; private set; }

    public void Start()
    {
        if (_isWorking == true)
            return;

        _progress = CurrentTimer;
        _contex.StartCoroutine(Countdown());
        _isWorking = true;
    }

    public void Stop()
    {
        _progress = 0;
        _contex.StopCoroutine(Countdown());
    }

    public void ResetT()
    {
        _progress = _timeMaximum;
        CurrentTimer = _timeMaximum;
    }

    public void Pause()
    {
        _progress = 0;
        _isWorking = false;
    }

    public IEnumerator Countdown()
    {
        while (_progress > 0)
        {
            _progress -= 1;
            CurrentTimer -= 1;
            OnTick?.Invoke(CurrentTimer);
            yield return new WaitForSeconds(1);
        }
    }
}
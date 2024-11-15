﻿using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<float> OnTick;

    private float _progress;
    private MonoBehaviour _contex;

    private bool _isWorking = false;

    public float TimeMaximum {  get; private set; }

    public Timer(float timeMaximum, MonoBehaviour contex)
    {
        TimeMaximum = timeMaximum;
        _contex = contex;

        if (TimeMaximum < 0)
            throw new ArgumentOutOfRangeException(nameof(timeMaximum));

        CurrentTimer = TimeMaximum;
        _progress = TimeMaximum;
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
        _progress = TimeMaximum;
        CurrentTimer = TimeMaximum;
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
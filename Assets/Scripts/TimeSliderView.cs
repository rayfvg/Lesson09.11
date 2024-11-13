using UnityEngine;
using UnityEngine.UI;

public class TimeSliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private float _timeMax;
    [SerializeField] private StartScene _contex;

    private Timer _timeSliderView;

    private void Awake()
    {
        _timeSliderView = new Timer(_timeMax, _contex);

        _timeSliderView.OnTick += OnTimerChancet;
    }

    private void OnDestroy()
    {
        _timeSliderView.OnTick -= OnTimerChancet;
    }

    private void OnTimerChancet(float time)
    {
        _slider.value = (time / _timeMax);
    }

    public void StartTimer() => _timeSliderView.Start();

    public void StopTimer() => _timeSliderView.Stop();

    public void ResetT()
    {
        _timeSliderView.ResetT();
        _slider.value = _timeSliderView.CurrentTimer / _timeMax;
    }

    public void Pause() => _timeSliderView.Pause();
}
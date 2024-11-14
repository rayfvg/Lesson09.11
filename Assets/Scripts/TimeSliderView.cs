using UnityEngine;
using UnityEngine.UI;

public class TimeSliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private float _timeMax;
    [SerializeField] private MonoBehaviour _contex;

    private Timer _timeSliderView;

    public void Initialize(Timer timer)
    {
        _timeSliderView = timer;

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
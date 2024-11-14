using System.Collections.Generic;
using UnityEngine;

public class ElementsTimerView : MonoBehaviour
{
    [SerializeField] private GameObject _hearåPrefab;
    [SerializeField] private GameObject _conteinerHeart;

    [SerializeField] private List<GameObject> _hearts = new List<GameObject>();

    private Timer _timeHeardView;

    public void Initialize(Timer timer)
    {
        _timeHeardView = timer;
        _timeHeardView.OnTick += OnTimerChancet;

        for (int i = 0; i < timer.TimeMaximum; i++)
        {
            GameObject heart = Instantiate(_hearåPrefab, transform.position, Quaternion.identity, _conteinerHeart.transform);
            _hearts.Add(heart);
        }
    }

    private void OnDestroy()
    {
        _timeHeardView.OnTick -= OnTimerChancet;
    }

    private void OnTimerChancet(float time)
    {
        for (int i = (int)_timeHeardView.TimeMaximum; i > 0; i--)
            _hearts[(int)time].gameObject.SetActive(false);
    }

    public void StartTimer() => _timeHeardView.Start();

    public void StopTimer() => _timeHeardView.Stop();

    public void ResetT()
    {
        _timeHeardView.ResetT();

        foreach (GameObject heard in _hearts)
            heard.gameObject.SetActive(true);
    }

    public void Pause() => _timeHeardView.Pause();
}
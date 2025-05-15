using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public bool IsRunning = false;
    [SerializeField] private float _startTime = 0f;
    public bool _countDown = false; // Count down or up
    private float _currentTime;

    public void TimerStart()
    {
        _currentTime = _startTime;
        StartTimer();
    }

    void Update()
    {
        if (!IsRunning) return;

        _currentTime += _countDown ? -Time.deltaTime : Time.deltaTime;

        if (_countDown && _currentTime <= 0f)
        {
            _currentTime = 0f;
            StopTimer();
        }

        UpdateTimerUI();
    }

    public void StartTimer()
    {
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public void ResetTimer()
    {
        _currentTime = _startTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((_currentTime * 1000f) % 1000f);
        GameManager.Instance.UiManager.UpdateTimerText(seconds,milliseconds);
    }

    public float GetTime()
    {
        return _currentTime;
    }
}

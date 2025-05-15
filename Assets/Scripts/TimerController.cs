using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
         // UI Text to display the timer
    public float startTime = 0f;   // Time in seconds
    public bool countDown = false; // Count down or up

    private float currentTime;
    private bool isRunning = false;

    public void TimerStart()
    {
        currentTime = startTime;
        StartTimer();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime += countDown ? -Time.deltaTime : Time.deltaTime;

        if (countDown && currentTime <= 0f)
        {
            currentTime = 0f;
            StopTimer();
        }

        UpdateTimerUI();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        GameManager.Instance.UiManager.UpdateTimerText(minutes,seconds);
    }

    public float GetTime()
    {
        return currentTime;
    }
}

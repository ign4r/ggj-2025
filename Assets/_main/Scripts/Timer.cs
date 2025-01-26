using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f; // Starting time in seconds
    private float currentTime;

    public Text timerText; // Optional: Drag your UI Text here for display

    public bool timerRunning = true; // Controls if the timer is active

    void Start()
    {
        currentTime = startTime; // Initialize the timer
    }

    void Update()
    {
        if (timerRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Subtract time every frame
            UpdateTimerUI();
        }
        else if (currentTime <= 0 && timerRunning)
        {
            currentTime = 0; // Clamp the timer at 0
            timerRunning = false;
            TimerEnded(); // Trigger the event for timer end
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Display as MM:SS format
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void TimerEnded()
    {
        Debug.Log("The countdown is complete!");
        // Add your logic here: load scenes, show UI, etc.
    }
}

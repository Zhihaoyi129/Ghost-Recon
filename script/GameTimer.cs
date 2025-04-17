using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Settings")]
    public float totalTime = 60f;
    public TextMeshProUGUI timerText;
    
    private float currentTime;
    private bool isTimerRunning = true;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        if (!isTimerRunning) return;
        
        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();
        
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isTimerRunning = false;
            GameManager.Instance.ShowFailure();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
            

            timerText.color = currentTime < 10f ? Color.red : Color.white;
        }
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Header("计时设置")]
    public float totalTime = 60f; // 总计时(秒)
    public TextMeshProUGUI timerText; // 计时显示文本
    
    [Header("结果画面")]
    public GameObject successPanel;
    public GameObject failPanel;
    
    private float currentTime;
    private bool gameEnded = false;
    
    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }
    
    void Update()
    {
        if(gameEnded) return;
        
        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();
        
        if(currentTime <= 0)
        {
            currentTime = 0;
            GameOver(false); // 时间耗尽，游戏失败
        }
    }
    
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        // 时间紧张时变红色
        if(currentTime < 10f)
        {
            timerText.color = Color.red;
        }
    }
    
    // 玩家通关时调用
    public void PlayerWin()
    {
        if(!gameEnded)
        {
            GameOver(true);
        }
    }
    
    void GameOver(bool isSuccess)
    {
        gameEnded = true;
        
        if(isSuccess)
        {
            successPanel.SetActive(true);
            // 可以在这里播放胜利音效
        }
        else
        {
            failPanel.SetActive(true);
            // 可以在这里播放失败音效
        }
        
        // 3秒后自动返回主菜单
        Invoke("ReturnToMainMenu", 3f);
    }
    
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // 替换为你的主菜单场景名
    }
}
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI coinCounterText;
    public int totalCoins = 6;

    [Header("Debug")]
    public bool enableDebugButtons = true;

    private int collectedCoins = 0;
    private int totalScore = 0;

    void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("CoinManager初始化完成");
        }
        else
        {
            Debug.LogWarning("检测到重复的CoinManager，已销毁", gameObject);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void CollectCoin(int value = 1)
    {
        collectedCoins++;
        totalScore += value;
        Debug.Log($"金币收集: {collectedCoins}/{totalCoins} (分数+{value})");

        UpdateUI();

        if (collectedCoins >= totalCoins)
        {
            AllCoinsCollected();
        }
    }

    void UpdateUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = $"Coins: {collectedCoins}/{totalCoins}\nScore: {totalScore}";
        }
        else
        {
            Debug.LogError("coinCounterText未分配！");
        }
    }

    void AllCoinsCollected()
    {
        Debug.Log($"<color=green>所有金币收集完成！最终分数: {totalScore}</color>");
        // 这里添加游戏胜利逻辑
    }

}
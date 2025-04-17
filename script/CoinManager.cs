using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    
    [Header("Settings")]
    public int totalCoins = 6;
    public TextMeshProUGUI coinCounterText;
    
    private int collectedCoins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectCoin(int value = 1)
    {
        collectedCoins += value;
        UpdateUI();
        
        if (collectedCoins >= totalCoins)
        {
            GameManager.Instance.ShowSuccess();
        }
    }

    private void UpdateUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = $"Coins: {collectedCoins}/{totalCoins}";
        }
    }

    public void ResetCoins()
    {
        collectedCoins = 0;
        UpdateUI();
    }
}
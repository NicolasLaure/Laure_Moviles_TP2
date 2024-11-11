using TMPro;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    private int _currentCoins = 0;

    void Start()
    {
        _currentCoins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = _currentCoins.ToString();
    }

    public static int GetCurrentCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }
    public static void AddCoins(int qty)
    {
        int currentCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", currentCoins + qty);
    }

    public static void RemoveCoins(int qty)
    {
        int currentCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", currentCoins - qty);
    }
}
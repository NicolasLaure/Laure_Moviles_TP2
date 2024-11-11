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
}
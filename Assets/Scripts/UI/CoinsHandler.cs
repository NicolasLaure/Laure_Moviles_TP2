using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private IntEventChannelSO onCoinsUpdated;
    [SerializeField] private IntEventChannelSO onCoinsAdd;
    [SerializeField] private IntEventChannelSO onCoinsRemove;

    void Start()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    private void OnEnable()
    {
        onCoinsAdd.onIntEvent += AddCoins;
        onCoinsRemove.onIntEvent += RemoveCoins;
    }

    private void OnDisable()
    {
        onCoinsAdd.onIntEvent -= AddCoins;
        onCoinsRemove.onIntEvent -= RemoveCoins;
    }

    public static int GetCurrentCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }

    public void AddCoins(int qty)
    {
        int currentCoins = GetCurrentCoins();
        SetCoins(currentCoins + qty);
    }

    public void RemoveCoins(int qty)
    {
        int currentCoins = GetCurrentCoins();
        SetCoins(currentCoins - qty);
    }

    private void SetCoins(int value)
    {
        PlayerPrefs.SetInt("Coins", value);
        onCoinsUpdated.RaiseEvent(value);
        coinsText.text = value.ToString();
    }
}
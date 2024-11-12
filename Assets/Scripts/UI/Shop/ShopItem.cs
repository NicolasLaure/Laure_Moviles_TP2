using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected Button buyButton;
    [SerializeField] protected Image buttonImage;
    [SerializeField] protected Color defaultColor;
    [SerializeField] protected Color lockedColor;
    [SerializeField] protected int price;
    [SerializeField] protected IntEventChannelSO onCoinsRemove;
    protected int _currentCoins;

    private void OnEnable()
    {
        buttonImage.color = defaultColor;
        _currentCoins = CoinsHandler.GetCurrentCoins();
        if (_currentCoins < price)
        {
            LockButton();
        }
    }

    private void LockButton()
    {
        buyButton.enabled = false;
        buttonImage.color = lockedColor;
    }

    public virtual void Buy()
    {
        onCoinsRemove.RaiseEvent(price);
        if (CoinsHandler.GetCurrentCoins() < price)
            LockButton();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPowerUp : ShopItem
{
    [SerializeField] private GameObject pillar;

    public override void Buy()
    {
        if (pillar.activeInHierarchy)
        {
            LockButton();
            return;
        }

        base.Buy();
        pillar.SetActive(true);
    }
}
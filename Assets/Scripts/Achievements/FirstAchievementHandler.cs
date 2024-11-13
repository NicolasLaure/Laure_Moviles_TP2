using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAchievementHandler : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onTrigger;
    [SerializeField] private AchievementSO achievement;
    [SerializeField] private string id;

    void Start()
    {
        int firstCoin = PlayerPrefs.GetInt(id, 0);
        if (firstCoin != 0)
        {
            Destroy(gameObject);
            return;
        }

        onTrigger.onVoidEvent += HandleFirst;
    }

    private void OnDisable()
    {
        onTrigger.onVoidEvent -= HandleFirst;
    }

    void HandleFirst()
    {
        AchievementsManager.UnlockAchievement(achievement);
        PlayerPrefs.SetInt(id, 1);
        Destroy(gameObject);
    }
}
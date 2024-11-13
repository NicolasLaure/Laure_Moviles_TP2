using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onLevelFinished;
    [SerializeField] private int levelNumber;
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private AchievementSO levelCompleted;

    private void OnEnable()
    {
        onLevelFinished.onVoidEvent += HandleLevelFinished;
    }

    private void OnDisable()
    {
        onLevelFinished.onVoidEvent -= HandleLevelFinished;
    }

    private void HandleLevelFinished()
    {
        PlayerPrefs.SetInt("MaxLevel", levelNumber + 1);
        AchievementsManager.UnlockAchievement(levelCompleted);
        Loader.ChangeScene(nextSceneIndex);
    }

    public void ReturnToMenu()
    {
        Loader.ChangeScene(1);
    }

    public void StartLevel(int levelNumber)
    {
        // Index 0 is splashScreen 1 is MainMenu
        int previousIndices = 1;
        Loader.ChangeScene(previousIndices + levelNumber);
    }
}
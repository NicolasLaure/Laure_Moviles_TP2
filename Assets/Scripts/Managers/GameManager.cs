using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onLevelFinished;

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
        Loader.ChangeScene(Loader.currentSceneIndex + 1);
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
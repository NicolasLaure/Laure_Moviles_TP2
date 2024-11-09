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
}
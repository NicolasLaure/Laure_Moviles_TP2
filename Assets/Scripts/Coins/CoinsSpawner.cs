using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float intervalBetweenCoins;

    private GameObject _currentCoin;

    private float timer = 0;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalBetweenCoins)
        {
            SpawnCoin();
            timer = 0;
        }
    }

    private void SpawnCoin()
    {
        if (_currentCoin != null && _currentCoin.activeInHierarchy)
            return;

        CoinsObjectPool.instance.TryGetPooledObject(out _currentCoin);
        _currentCoin.transform.position = GetRandomPos();
    }

    private Vector2 GetRandomPos()
    {
        float randomX = Random.Range(-bounds.x / 2, bounds.x / 2);
        float randomY = Random.Range(-bounds.y / 2, bounds.y / 2);
        return new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }
}
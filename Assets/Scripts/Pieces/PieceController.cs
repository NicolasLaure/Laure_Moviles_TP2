using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PieceController : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onPlayerPositionChanged;
    [SerializeField] private VoidEventChannelSO onPieceLanded;
    [SerializeField] private PieceConfigSO config;
    [SerializeField] private float lowestLimit;
    private bool _isFalling;
    private Rigidbody2D _rb;

    public bool IsFalling => _isFalling;
    private float fallingSpeed;
    private void OnEnable()
    {
        fallingSpeed = config.speed;
        transform.rotation = quaternion.identity;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _isFalling = true;

        onPlayerPositionChanged.onVector3Event += HandlePlayerPositionChanged;
    }

    private void OnDisable()
    {
        _rb.gravityScale = 1;
        _isFalling = false;
        onPlayerPositionChanged.onVector3Event -= HandlePlayerPositionChanged;
    }

    private void Update()
    {
        if (_isFalling)
        {
            transform.position += Vector3.down * (fallingSpeed * Time.deltaTime);
        }

        if (transform.position.y < lowestLimit)
        {
            if (_isFalling)
                onPieceLanded?.RaiseEvent();

            gameObject.SetActive(false);
        }
    }

    public void FastFall()
    {
        fallingSpeed = config.fastFallspeed;
    }
    private void HandlePlayerPositionChanged(Vector3 playerPosition)
    {
        transform.position = new Vector3(playerPosition.x, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isFalling)
        {
            _isFalling = false;
            _rb.gravityScale = 1;
            onPlayerPositionChanged.onVector3Event -= HandlePlayerPositionChanged;
            onPieceLanded?.RaiseEvent();
        }
    }
}
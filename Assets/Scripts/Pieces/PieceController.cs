using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PieceController : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onPlayerPositionChanged;
    [SerializeField] private Vector3EventChannelSO onPieceLanded;
    [SerializeField] private PieceConfigSO config;
    private bool _isFalling;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        _isFalling = true;

        onPlayerPositionChanged.onVector3Event += HandlePlayerPositionChanged;
    }

    private void OnDisable()
    {
        rb.gravityScale = 1;
        _isFalling = false;
        onPlayerPositionChanged.onVector3Event -= HandlePlayerPositionChanged;
    }

    private void Update()
    {
        if (_isFalling)
            transform.position += Vector3.down * config.speed * Time.deltaTime;
    }

    private void HandlePlayerPositionChanged(Vector3 playerPosition)
    {
        transform.position = new Vector3(playerPosition.x, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isFalling)
        {
            enabled = false;
            onPieceLanded?.RaiseEvent(transform.position);
        }
    }
}
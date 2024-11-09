using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onMoveChannel;
    [SerializeField] private Vector3EventChannelSO onPlayerPositionChanged;
    [SerializeField] private VoidEventChannelSO onRelease;
    [SerializeField] private VoidEventChannelSO onPieceLanded;
    [SerializeField] private Vector2 minMaxHorizontalBounds;
    [SerializeField] private float lateralDisplacement;
    [SerializeField] private float moveCoolDown;
    private Vector2 _dir;

    private Coroutine _spawnCoroutine;
    private GameObject _currentPiece;
    private Coroutine _moveCoolDownCoroutine;
    private bool _canMove = true;

    void Start()
    {
        onMoveChannel.onVector3Event += HandleMove;
        onPieceLanded.onVoidEvent += HandlePieceLanded;
        onRelease.onVoidEvent += HandleRelease;
        SpawnNextPiece();
    }

    private void OnDestroy()
    {
        onMoveChannel.onVector3Event -= HandleMove;
        onPieceLanded.onVoidEvent -= HandlePieceLanded;
        onRelease.onVoidEvent -= HandleRelease;
    }

    private void HandleMove(Vector3 inputDir)
    {
        _dir = inputDir;
        Move(_dir.x);
    }

    private void HandleRelease()
    {
        _dir = Vector3.zero;
    }

    private IEnumerator SpawnPieceCoroutine()
    {
        yield return null;
        if (PiecesPool.instance.TryGetPooledObject(out GameObject piece))
        {
            piece.transform.position = transform.position;
            piece.GetComponent<PieceController>().enabled = true;
            _currentPiece = piece;
        }
    }

    public void Move(float horizontalDir)
    {
        if (!_canMove)
            return;

        float newX = Mathf.Clamp(transform.position.x + (horizontalDir * lateralDisplacement), minMaxHorizontalBounds.x, minMaxHorizontalBounds.y);
        if (newX != transform.position.x)
        {
            transform.position = new Vector3(newX, transform.position.y, 0);
            onPlayerPositionChanged?.RaiseEvent(transform.position);
        }

        if (_moveCoolDownCoroutine != null)
            StopCoroutine(_moveCoolDownCoroutine);

        _moveCoolDownCoroutine = StartCoroutine(HandleMoveCoolDown());
    }

    private IEnumerator HandleMoveCoolDown()
    {
        _canMove = false;
        yield return new WaitForSeconds(moveCoolDown);
        _canMove = true;
    }

    private void HandlePieceLanded()
    {
        SpawnNextPiece();
    }

    private void SpawnNextPiece()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = StartCoroutine(SpawnPieceCoroutine());
    }

    public void RotateCurrentPieceLeft()
    {
        if (_currentPiece == null)
            return;

        _currentPiece.transform.rotation *= Quaternion.Euler(0, 0, -90);
    }

    public void RotateCurrentPieceRight()
    {
        if (_currentPiece == null)
            return;

        _currentPiece.transform.rotation *= Quaternion.Euler(0, 0, 90);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2ChannelEvent onSwipe;
    [SerializeField] private Vector3EventChannelSO onPlayerPositionChanged;
    [SerializeField] private VoidEventChannelSO onRelease;
    [SerializeField] private VoidEventChannelSO onPieceLanded;
    [SerializeField] private BoolEventChannelSO onFinishLineTouch;
    [SerializeField] private Vector2 minMaxHorizontalBounds;
    [SerializeField] private float lateralDisplacement;
    [SerializeField] private float moveCoolDown;
    [SerializeField] private float maxHorizontalAngle;
    private Vector2 _dir;

    private Coroutine _spawnCoroutine;
    private GameObject _currentPiece;
    private Coroutine _moveCoolDownCoroutine;
    private bool _canMove = true;
    private bool _canSpawn = true;

    void Start()
    {
        _canSpawn = true;
        onSwipe.onVector2Event += HandleSwipe;
        onPieceLanded.onVoidEvent += HandlePieceLanded;
        onRelease.onVoidEvent += HandleRelease;
        onFinishLineTouch.onBoolEvent += SetCanSpawn;
        SpawnNextPiece();
    }

    private void OnDestroy()
    {
        onSwipe.onVector2Event -= HandleSwipe;
        onPieceLanded.onVoidEvent -= HandlePieceLanded;
        onRelease.onVoidEvent -= HandleRelease;
        onFinishLineTouch.onBoolEvent -= SetCanSpawn;
    }

    private void HandleSwipe(Vector2 swipeDir)
    {
        if (swipeDir.x < -Mathf.Cos(maxHorizontalAngle * Mathf.Deg2Rad) || swipeDir.x > Mathf.Cos(maxHorizontalAngle * Mathf.Deg2Rad))
        {
            HandleMove(new Vector2(swipeDir.x > 0 ? 1 : -1, 0));
        }
        else
        {
            if (swipeDir.y > 0)
                RotateCurrentPieceLeft();
            else
                DropPiece();
        }
    }

    private void HandleMove(Vector2 inputDir)
    {
        _dir = inputDir;
        Move(_dir.x);
    }

    private void HandleRelease()
    {
        _dir = Vector2.zero;
    }

    private IEnumerator SpawnPieceCoroutine()
    {
        yield return null;
        GameObject piece = PiecesPool.instance.GetRandomPooledObject();
        piece.transform.position = transform.position;
        piece.GetComponent<PieceController>().enabled = true;
        _currentPiece = piece;
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
        _currentPiece = null;
        SpawnNextPiece();
    }

    private void SpawnNextPiece()
    {
        if (!_canSpawn)
            return;

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

    private void DropPiece()
    {
        if (_currentPiece == null)
            return;

        _currentPiece.GetComponent<PieceController>().FastFall();
    }

    private void SetCanSpawn(bool value)
    {
        _canSpawn = !value;
        if (_canSpawn)
            SpawnNextPiece();
    }
}
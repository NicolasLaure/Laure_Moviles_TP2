using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onMoveChannel;
    [SerializeField] private Vector3EventChannelSO onPlayerPositionChanged;
    [SerializeField] private VoidEventChannelSO onRelease;
    [SerializeField] private VoidEventChannelSO onPieceLanded;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 minMaxHorizontalBounds;

    private Vector2 _dir;

    private float horizontalDisplacement;

    private Coroutine spawnCoroutine;

    void Start()
    {
        onMoveChannel.onVector3Event += HandleMove;
        onPieceLanded.onVoidEvent += SpawnNextPiece;
        onRelease.onVoidEvent += HandleRelease;
        SpawnNextPiece();
    }

    private void OnDestroy()
    {
        onMoveChannel.onVector3Event -= HandleMove;
        onPieceLanded.onVoidEvent -= SpawnNextPiece;
        onRelease.onVoidEvent -= HandleRelease;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Clamp(transform.position.x + (horizontalDisplacement * speed * Time.deltaTime), minMaxHorizontalBounds.x, minMaxHorizontalBounds.y);
        if (newX != transform.position.x)
        {
            transform.position = new Vector3(newX, transform.position.y, 0);
            onPlayerPositionChanged?.RaiseEvent(transform.position);
        }
    }

    private void HandleMove(Vector3 inputDir)
    {
        _dir = inputDir;
        horizontalDisplacement = _dir.x;
    }

    private void HandleRelease()
    {
        _dir = Vector3.zero;
        horizontalDisplacement = _dir.x;
    }

    private IEnumerator SpawnPieceCoroutine()
    {
        yield return null;
        if (PiecesPool.instance.TryGetPooledObject(out GameObject piece))
        {
            piece.transform.position = transform.position;
            piece.GetComponent<PieceController>().enabled = true;
        }
    }

    private void SpawnNextPiece()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnPieceCoroutine());
    }
}
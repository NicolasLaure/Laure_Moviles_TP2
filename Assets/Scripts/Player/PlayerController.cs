using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onMoveChannel;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 minMaxHorizontalBounds;

    private Vector2 _dir;

    private float horizontalDisplacement;

    void Start()
    {
        onMoveChannel.onVector3Event += HandleMove;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Clamp(transform.position.x + (horizontalDisplacement * speed * Time.deltaTime), minMaxHorizontalBounds.x, minMaxHorizontalBounds.y);
        transform.position = new Vector3(newX, transform.position.y, 0);
    }

    private void HandleMove(Vector3 inputDir)
    {
        _dir = inputDir;
        horizontalDisplacement = _dir.x;
    }
}
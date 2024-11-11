using System;
using UnityEngine;

public class LateralDisplacementManager : MonoBehaviour
{
    [SerializeField] private float lowerLimit;
    [SerializeField] private float upperLimit;
    [SerializeField] private float horizontalOffset;

    [SerializeField] private float verticalSpeed;
    [SerializeField] private float windForce;
    [SerializeField] private Gyro gyro;
    [SerializeField] private float playerLateralForce;
    private float _lateralForce;

    private void Start()
    {
        _lateralForce = windForce;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        TryPushObject();
    }

    private void LateUpdate()
    {
        UpdateLateralForce(playerLateralForce * Mathf.Sin(gyro.ZRotation * Mathf.Deg2Rad));
    }

    private void Move()
    {
        if (transform.position.y >= upperLimit)
            transform.position = new Vector2(transform.position.x, lowerLimit);

        transform.Translate(Vector2.up * (verticalSpeed * Time.deltaTime));
    }

    public void UpdateLateralForce(float lateralForce)
    {
        _lateralForce = windForce + lateralForce;
    }

    private void TryPushObject()
    {
        Vector2 origin = transform.position;
        Vector2 offset = new Vector2(horizontalOffset, 0);
        origin += _lateralForce > 0 ? -offset : offset;

        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lateralForce > 0 ? 1 : -1, 0));
        if (hit.transform == null)
            return;

        PieceController pieceRigidBody = hit.transform.GetComponentInParent<PieceController>();
        if (pieceRigidBody != null)
        {
            pieceRigidBody.HandleLateralForce(new Vector2(_lateralForce, 0));
        }
    }
}
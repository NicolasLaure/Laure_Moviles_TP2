using System;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private Vec2FloatEvent onTouch;
    [SerializeField] private Vec2FloatEvent onRelease;

    private PlayerInputActions _input;

    void Awake()
    {
        _input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _input.Player.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        onTouch?.RaiseEvent(Utils.Utils.ScreenToWorld(Camera.main, _input.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        onRelease?.RaiseEvent(Utils.Utils.ScreenToWorld(Camera.main, _input.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return _input.Player.PrimaryPosition.ReadValue<Vector2>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputReader : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onMove;
    [SerializeField] private Vector3EventChannelSO onTouch;
    [SerializeField] private VoidEventChannelSO onRelease;

    private PlayerInputActions _input;

    void Start()
    {
        _input = new PlayerInputActions();
        _input.Enable();

        _input.Player.Move.performed += HandleMove;
        _input.Player.Tap.started += HandleTouch;
        _input.Player.Touch.canceled += HandleRelease;
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        onMove?.RaiseEvent(context.ReadValue<Vector2>());
    }

    public void HandleTouch(InputAction.CallbackContext context)
    {
        onTouch?.RaiseEvent(context.ReadValue<Vector2>());
    }

    public void HandleRelease(InputAction.CallbackContext context)
    {
        onRelease?.RaiseEvent();
    }
}
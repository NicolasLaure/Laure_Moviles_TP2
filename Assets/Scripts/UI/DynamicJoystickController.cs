using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

public class DynamicJoystickController : MonoBehaviour
{
    [SerializeField] private Vector3EventChannelSO onTouchChannel;
    [SerializeField] private VoidEventChannelSO onReleaseChannel;

    [SerializeField] private RectTransform canvasRect;

    [SerializeField] private RectTransform joystick;

    private void Start()
    {
        onTouchChannel.onVector3Event += HandleTouch;
        onReleaseChannel.onVoidEvent += HandleRelease;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        onTouchChannel.onVector3Event -= HandleTouch;
        onReleaseChannel.onVoidEvent -= HandleRelease;
    }

    private void HandleTouch(Vector3 pos)
    {
        gameObject.SetActive(true);
        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector2 normalizedPos = new Vector2(pos.x / canvasRect.localScale.x, pos.y / canvasRect.localScale.y);
        Vector2 screenPosition = new Vector2(normalizedPos.x - canvasRect.sizeDelta.x * 0.5f, normalizedPos.y - canvasRect.sizeDelta.y * 0.5f);

        rectTransform.localPosition = screenPosition;
    }

    private void HandleRelease()
    {
        gameObject.SetActive(false);
        joystick.localPosition = Vector3.zero;
    }
}
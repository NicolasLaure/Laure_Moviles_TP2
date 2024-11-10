using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader reader;
    [SerializeField] private Vec2FloatEvent onTouch;
    [SerializeField] private Vec2FloatEvent onRelease;
    [SerializeField] private Vector2ChannelEvent onSwipe;

    [Header("config")]
    [SerializeField] private float minSwipeDistance;
    [SerializeField] private float maxSwipeTime;

    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    private void OnEnable()
    {
        onTouch.onVector2FloatEvent += SwipeStart;
        onRelease.onVector2FloatEvent += SwipeEnd;
    }

    private void OnDisable()
    {
        onTouch.onVector2FloatEvent -= SwipeStart;
        onRelease.onVector2FloatEvent -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) < minSwipeDistance || _endTime - _startTime > maxSwipeTime)
            return;

        Debug.DrawLine(_startPosition, _endPosition, Color.red, 1.0f);
        onSwipe?.RaiseEvent((_endPosition - _startPosition).normalized);
    }
}
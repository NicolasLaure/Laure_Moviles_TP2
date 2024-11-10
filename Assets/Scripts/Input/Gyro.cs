using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    private Gyroscope _gyroscope;
    private Quaternion _phoneRotation;
    private Quaternion calibratedRotation;
    private bool _isActive;

    public Quaternion Rotation => calibratedRotation;

    public void EnableGyro()
    {
        if (_gyroscope != null)
            return;

        if (SystemInfo.supportsGyroscope)
        {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
        }

        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
            _phoneRotation = calibratedRotation * Quaternion.Inverse(_gyroscope.attitude);
    }

    public void Calibrate()
    {
        calibratedRotation = _gyroscope.attitude;
    }
}
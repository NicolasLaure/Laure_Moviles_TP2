using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    private Gyroscope _gyroscope;
    private float zAxisRotation;
    private float zAxisCalibrationRotation;
    private bool _isActive;

    public float ZRotation => zAxisRotation;

    public void EnableGyro()
    {
        if (_gyroscope != null)
            return;

        if (SystemInfo.supportsGyroscope)
        {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
            _isActive = true;
        }
    }

    private void Start()
    {
        EnableGyro();
    }

    private void Update()
    {
        if (_isActive)
        {
            zAxisRotation = zAxisCalibrationRotation - _gyroscope.attitude.eulerAngles.z;
        }
    }

    public float GetZAxisAngle()
    {
        return _gyroscope.rotationRateUnbiased.z;
    }

    public void Calibrate()
    {
        zAxisCalibrationRotation = _gyroscope.attitude.eulerAngles.z;
    }
}
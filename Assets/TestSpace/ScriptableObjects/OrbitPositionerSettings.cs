using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OribitPositionerSettings")]
public class OrbitPositionerSettings : ScriptableObject
{
    public float _offsetRadius;
    [Range(0, 360)] public float _offsetAngle;
    [Range(0, 360)] public float _plasementAngle;

    [Header("Rotate around:")]
    public bool _x = true;
    public bool _y;
    public bool _z;
    [Header("Start position:")]
    public bool _up = true;
    public bool _right;
    public bool _forward;

    private Vector3 startSide;
    private Vector3 rotatableAxis;

    public Vector3 GetStartSide()
    {
        return startSide;
    }

    public Vector3 GetRotatableAxis()
    {
        return rotatableAxis;
    }

    private void OnValidate()
    {
        CheckStartPosition();
        CheckRotationAxis();
    }

    private void CheckRotationAxis()
    {
        if (_x)
        {
            rotatableAxis = Vector3.right;

            _y = false;
            _z = false;
        }

        if (_y)
        {
            rotatableAxis = Vector3.up;

            _x = false;
            _z = false;
        }

        if (_z)
        {
            rotatableAxis = Vector3.forward;

            _x = false;
            _y = false;
        }
    }

    private void CheckStartPosition()
    {
        if (_up)
        {
            startSide = Vector3.up;

            _right = false;
            _forward = false;
        }

        if (_right)
        {
            startSide = Vector3.right;

            _up = false;
            _forward = false;
        }

        if (_forward)
        {
            startSide = Vector3.forward;

            _up = false;
            _right = false;
        }
    }
}

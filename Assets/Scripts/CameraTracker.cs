using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTracker : MonoBehaviour
{
    public static Camera Current;

    Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        tryUpdateCamera();
    }

    void OnEnable()
    {
        tryUpdateCamera();
    }

    void tryUpdateCamera()
    {
        if (_camera != null && _camera.enabled)
        {
            Current = _camera;
        }
    }
}

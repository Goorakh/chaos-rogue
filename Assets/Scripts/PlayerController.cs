using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;

    public Transform AimCircle;

    public float MoveSpeed;
    public float SprintSpeedMultiplier;

    public float SprintAcceleration;
    public float SprintDecceleration;

    float _currentSprintSpeedMultiplier = 1f;

    void Update()
    {
        Vector3 movementInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movementInput.z += 1f;

        if (Input.GetKey(KeyCode.S))
            movementInput.z -= 1f;

        if (Input.GetKey(KeyCode.D))
            movementInput.x += 1f;

        if (Input.GetKey(KeyCode.A))
            movementInput.x -= 1f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_currentSprintSpeedMultiplier != SprintSpeedMultiplier)
            {
                _currentSprintSpeedMultiplier = Mathf.MoveTowards(_currentSprintSpeedMultiplier, SprintSpeedMultiplier, SprintAcceleration * Time.deltaTime);
            }
        }
        else
        {
            if (_currentSprintSpeedMultiplier != 1f)
            {
                _currentSprintSpeedMultiplier = Mathf.MoveTowards(_currentSprintSpeedMultiplier, 1f, SprintDecceleration * Time.deltaTime);
            }
        }

        transform.position += movementInput.normalized * (MoveSpeed * _currentSprintSpeedMultiplier * Time.deltaTime);

        Vector3 localEulerAngles = AimCircle.transform.localEulerAngles;
        localEulerAngles.y = -MathUtils.AngleTo(Camera.WorldToScreenPoint(AimCircle.transform.position), Input.mousePosition);
        AimCircle.transform.localEulerAngles = localEulerAngles;
    }
}

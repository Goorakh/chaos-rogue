using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;

    public float MoveSpeed;
    public float SprintSpeedMultiplier;

    public float SprintAcceleration;
    public float SprintDecceleration;

    float _currentSprintSpeedMultiplier = 1f;

    HealthMixin _healthMixin;

    void Awake()
    {
        _healthMixin = GetComponent<HealthMixin>();
    }

    void Update()
    {
        if (_healthMixin.IsKilled)
            return;

        if (Input.GetKey(KeyCode.G))
        {
            _healthMixin.Damage(5f * Time.deltaTime);
        }

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
    }
}

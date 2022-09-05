using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMixin : MonoBehaviour
{
    public event Action Damaged;
    public event Action Killed;

    public float MaxHealth;

    public bool IsKilled { get; private set; }
    public float CurrentHealthFraction { get; private set; } = 1f;

    public void Damage(float amount)
    {
        if (amount <= 0f)
            return;

        CurrentHealthFraction -= amount / MaxHealth;
        Damaged();

        if (CurrentHealthFraction <= 0f)
        {
            CurrentHealthFraction = 0f;
            IsKilled = true;
            Killed();
        }
    }
}

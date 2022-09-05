using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCircleController : MonoBehaviour
{
    public HealthMixin HealthMixin;

    public bool LerpColorOnDamage;

    public Renderer Renderer;
    public Color FullHealthColor;
    public Color NoHealthColor;

    void Awake()
    {
        if (LerpColorOnDamage)
        {
            HealthMixin.Damaged += OnDamage;
            HealthMixin.Killed += OnKilled;

            Renderer.material.SetColor(ShaderIDs._Color, FullHealthColor);
        }
    }

    void OnKilled()
    {
        gameObject.SetActive(false);
    }

    void OnDamage()
    {
        Renderer.material.SetColor(ShaderIDs._Color, Color.Lerp(FullHealthColor, NoHealthColor, 1f - HealthMixin.CurrentHealthFraction));
    }

    void Update()
    {
        if (CameraTracker.Current == null)
            return;

        Vector3 localEulerAngles = transform.localEulerAngles;
        localEulerAngles.y = -MathUtils.AngleTo(CameraTracker.Current.WorldToScreenPoint(transform.position), Input.mousePosition);
        transform.localEulerAngles = localEulerAngles;
    }
}

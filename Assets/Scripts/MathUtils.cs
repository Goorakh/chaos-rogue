using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static float AngleTo(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}

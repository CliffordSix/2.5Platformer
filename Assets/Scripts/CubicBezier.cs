using UnityEngine;
using System.Collections.Generic;

public class CubicBezier
{
    Vector3[] points;

    public CubicBezier(Vector3[] points)
    {
        this.points = points;
    }

    public Vector3 Get(float t)
    {
        float x = 1 - t;
        Vector3 a = Mathf.Pow(x, 3) * points[0];
        Vector3 b = 3 * Mathf.Pow(x, 2) * t * points[1];
        Vector3 c = 3 * x * Mathf.Pow(t, 2) * points[2];
        Vector3 d = Mathf.Pow(t, 3) * points[3];
        return a + b + c + d;
    }
}

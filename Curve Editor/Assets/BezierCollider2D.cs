using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EdgeCollider2D))]
public class BezierCollider2D : MonoBehaviour
{
    public Vector2 q;
    public Vector2 y;

    public Vector2 u;
    public Vector2 v;

    public int z;

    Vector3 CalculateBezierPoint(float u, Vector3 p0, Vector3 handlerP0, Vector3 handlerP1, Vector3 p1)
    {
        float t = 1.0f - u;
        float uu = u * u;
        float tt = t * t;
        float ttt = tt * t;
        float uuu = uu * u;

        Vector3 p = ttt * p0; //first term
        p += 3f * tt * u * handlerP0; //second term
        p += 3f * t * uu * handlerP1; //third term
        p += uuu * p1; //fourth term

        return p;
    }

    public Vector2[] calculate2DPoints()
    {
        List<Vector2> points = new List<Vector2>();

        points.Add(q);
        for (int i = 1; i < z; i++) // for all the points inbetween the first and last
        {
            points.Add(CalculateBezierPoint((1f / z) * i, q, u, v, y));
        }
        points.Add(y);

        return points.ToArray();
    }

}
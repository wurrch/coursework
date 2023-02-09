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

    Vector3 CalculateBezierPoint(float pointsQuantityProportion, Vector3 p0, Vector3 handlerP0, Vector3 handlerP1, Vector3 p1)
    {
        float inversePointsQuantityProportion = 1.0f - pointsQuantityProportion;
        float pointsQuantityProportionSquared = pointsQuantityProportion * pointsQuantityProportion;
        float inversePointsQuantityProportionSquared = inversePointsQuantityProportion * inversePointsQuantityProportion;
        float uuu = inversePointsQuantityProportionSquared * inversePointsQuantityProportion;
        float ttt = pointsQuantityProportionSquared * pointsQuantityProportion;

        Vector3 p = uuu * p0; //first term
        p += 3f * inversePointsQuantityProportionSquared * pointsQuantityProportion * handlerP0; //second term
        p += 3f * inversePointsQuantityProportion * pointsQuantityProportionSquared * handlerP1; //third term
        p += ttt * p1; //fourth term

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
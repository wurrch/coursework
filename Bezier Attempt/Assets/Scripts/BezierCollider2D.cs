using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class BezierCollider2D : MonoBehaviour
{
    public int pointsAmount;
    public Vector2 firstPoint;
    public Vector2 firstHandle;
    public Vector2 lastPoint;
    public Vector2 lastHandle;

    public GameObject test;

    void Start()
    {
    }
    void Update()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(firstPoint);
        for (int i = 1; i < pointsAmount; i++)
        {
            points.Add(CalculatePointBetween((1.0f / pointsAmount) * i, firstPoint, firstHandle, lastPoint, lastHandle));
        }
        points.Add(lastPoint);

        foreach (Vector2 point in points){
            GameObject.Instantiate(test, new Vector3(point.x, point.y, 0), Quaternion.identity);
        }
    }

    Vector3 CalculatePointBetween(float pointsProportion, Vector3 firstPoint, Vector3 firstHandle, Vector3 lastPoint, Vector3 lastHandle)
    {

        Vector3 pointCoord = (float)Math.Pow(pointsProportion, 3) * firstPoint;
        pointCoord += 3.0f * (float)Math.Pow(1.0f - pointsProportion, 2) * firstHandle;
        pointCoord += 3.0f * (float)Math.Pow(pointsProportion, 2) * (1.0f - pointsProportion) * lastHandle;
        pointCoord += (float)Math.Pow(pointsProportion, 3) * lastPoint;

        return pointCoord;
    }
}

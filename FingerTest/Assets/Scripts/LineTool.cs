
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTool : MonoBehaviour
{
    public void createPoint(List<GameObject> points, GameObject pointPrefab)
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchPosition.z = 0f;

        GameObject point = Instantiate(pointPrefab, touchPosition, Quaternion.identity);
        points.Add(point);
    }

    public void createLine(List<GameObject> points , GameObject linePrefab)
    {
        Vector3 point0 = points[StartScript.pointIteration].transform.position;
        Vector3 point1 = points[StartScript.pointIteration + 1].transform.position;

        Vector3 middlePoint = new Vector3();

        middlePoint.x = (point0.x + point1.x) / 2;
        middlePoint.y = (point0.y + point1.y) / 2;

        double length = Math.Sqrt(Math.Pow(Math.Abs(point0.x - point1.x), 2.0) + Math.Pow(Math.Abs(point0.y - point1.y), 2.0));
        double angle = Math.Atan((point0.y - point1.y) / (point0.x - point1.x)) * (180 / Math.PI);

        GameObject line = Instantiate(linePrefab, middlePoint, Quaternion.identity);
        line.transform.localScale = new Vector3((float)length, 0.3f, 1);
        line.transform.eulerAngles = new Vector3(0, 0, (float)angle);
    }
}

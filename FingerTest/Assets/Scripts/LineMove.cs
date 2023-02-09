using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    bool touchLocked = false;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (touchLocked == false)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    List<GameObject> points = StartScript.points;

                    Vector3 point0 = points[StartScript.pointIteration].transform.position;
                    Vector3 point1 = points[StartScript.pointIteration + 1].transform.position;

                    Vector3 middlePoint = new Vector3();
                    middlePoint.x = (point0.x + point1.x) / 2;
                    middlePoint.y = (point0.y + point1.y) / 2;

                    double length = Math.Sqrt(Math.Pow(Math.Abs(point0.x - point1.x), 2.0) + Math.Pow(Math.Abs(point0.y - point1.y), 2.0));
                    double angle = Math.Atan((point0.y - point1.y) / (point0.x - point1.x)) * (180 / Math.PI);

                    transform.position = middlePoint;
                    transform.localScale = new Vector3((float)length, 0.3f, 1);
                    transform.eulerAngles = new Vector3(0, 0, (float)angle);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    StartScript.pointIteration++;
                }
            }
        }
        else
        {
            touchLocked = true;
        }
    }
}

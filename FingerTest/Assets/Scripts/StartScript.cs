using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    bool touchLocked = false;
    public GameObject pointPrefab;
    public GameObject linePrefab;
    public static List<GameObject> points = new List<GameObject>();
    LineTool createPointScript = new LineTool();
    public static int pointIteration = 0;
    public GameObject characterPrefab;
    void Start()
    {
        Application.targetFrameRate = 1000;

        print("Balls");
        GameObject character = GameObject.Instantiate(characterPrefab, new Vector3(0,100,0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (touchLocked == false)
            {
                touchLocked = true;

                // Create a point when the user taps
                createPointScript.createPoint(points, pointPrefab);

                if (points.Count > 1)
                {
                    createPointScript.createLine(points, linePrefab);
                }
            }
        }
        else
        {
            touchLocked = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class BezierCollider2D : MonoBehaviour
{
    public bool selectedMode = false;

    public GameObject controlPrefab;
    public GameObject handlePrefab;
    GameObject firstControlBall;
    GameObject firstHandleBall;
    GameObject lastControlBall;
    GameObject lastHandleBall;

    public int pointsAmount = 0;
    public Vector2 firstPoint = Vector2.zero;
    public Vector2 firstHandle = Vector2.zero;
    public Vector2 lastPoint = Vector2.zero;
    public Vector2 lastHandle = Vector2.zero;

    int prevPointsAmount = 0;
    Vector2 prevFirstPoint = Vector2.zero;
    Vector2 prevFirstHandle = Vector2.zero;
    Vector2 prevLastPoint = Vector2.zero;
    Vector2 prevLastHandle = Vector2.zero;

    EdgeCollider2D edgeCollider;
    LineRenderer lineRenderer;

    public void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();

        firstControlBall = GameObject.Instantiate(controlPrefab, new Vector3(firstPoint.x, firstPoint.y, 0), Quaternion.identity);
        firstHandleBall = GameObject.Instantiate(handlePrefab, new Vector3(firstHandle.x, firstHandle.y, 0), Quaternion.identity);
        lastControlBall = GameObject.Instantiate(controlPrefab, new Vector3(lastPoint.x, lastPoint.y, 0), Quaternion.identity);
        lastHandleBall = GameObject.Instantiate(handlePrefab, new Vector3(lastHandle.x, lastHandle.y, 0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (IsTapOnObject() ||
            firstControlBall.GetComponent<BallController>().IsTapOnObject() ||
            firstHandleBall.GetComponent<BallController>().IsTapOnObject() ||
            lastControlBall.GetComponent<BallController>().IsTapOnObject() ||
            lastHandleBall.GetComponent<BallController>().IsTapOnObject()){
                selectedMode = true;
                firstControlBall.SetActive(true);
                firstHandleBall.SetActive(true);
                lastControlBall.SetActive(true);
                lastHandleBall.SetActive(true);
            }
            else{
                selectedMode = false;
                firstControlBall.SetActive(false);
                firstHandleBall.SetActive(false);
                lastControlBall.SetActive(false);
                lastHandleBall.SetActive(false);
            }
        }

        firstPoint = new Vector2(firstControlBall.transform.position.x, firstControlBall.transform.position.y);
        firstHandle = new Vector2(firstHandleBall.transform.position.x, firstHandleBall.transform.position.y);
        lastPoint = new Vector2(lastControlBall.transform.position.x, lastControlBall.transform.position.y);
        lastHandle = new Vector2(lastHandleBall.transform.position.x, lastHandleBall.transform.position.y);

        if (pointsAmount > 0 &&
        (prevPointsAmount != pointsAmount ||
        prevFirstPoint != firstPoint ||
        prevFirstHandle != firstHandle ||
        prevLastPoint != lastPoint ||
        prevLastHandle != lastHandle)) // If any of the values are not the same as the last values:
        {
            firstPoint = firstControlBall.transform.position;

            prevPointsAmount = pointsAmount;
            prevFirstPoint = firstPoint;
            prevFirstHandle = firstHandle;
            prevLastPoint = lastPoint;
            prevLastHandle = lastHandle;

            List<Vector2> points = new List<Vector2>();
            points.Add(firstPoint);
            for (int i = 1; i < pointsAmount; i++)
            {
                points.Add(CalculatePointBetween((1.0f / pointsAmount) * i, firstPoint, firstHandle, lastPoint, lastHandle));
            }
            points.Add(lastPoint);

            Vector2[] pointsArray = points.ToArray();
            edgeCollider.points = pointsArray;

            lineRenderer.positionCount = pointsAmount + 1;
            for (int i = 0; i <= pointsAmount; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(pointsArray[i].x, pointsArray[i].y));
            }
        }
    }

    Vector3 CalculatePointBetween(float pointsProportion, Vector3 firstPoint, Vector3 firstHandle, Vector3 lastPoint, Vector3 lastHandle)
    {
        Vector3 pointCoord = (float)Math.Pow(1.0f - pointsProportion, 3) * firstPoint;
        pointCoord += 3.0f * (float)Math.Pow(1.0f - pointsProportion, 2) * pointsProportion * firstHandle;
        pointCoord += 3.0f * (float)Math.Pow(pointsProportion, 2) * (1.0f - pointsProportion) * lastHandle;
        pointCoord += (float)Math.Pow(pointsProportion, 3) * lastPoint;

        return pointCoord;
    }

    bool IsTapOnObject(){
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject){
            return true;
        }
        return false;
    }
}

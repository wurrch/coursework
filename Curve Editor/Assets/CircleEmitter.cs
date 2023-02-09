using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEmitter : MonoBehaviour
{
    public GameObject circlePrefab;
    void Start()
    {
        Application.targetFrameRate = 1000;
    }

    void Update()
    {
        GameObject.Instantiate(circlePrefab);
    }
}

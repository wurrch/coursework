using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CenterPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height * 8/10, 0) );
        CenterPos.z = 0;
        transform.position = CenterPos;
    }
}

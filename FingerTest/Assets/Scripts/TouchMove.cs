using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    bool touchLocked = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (touchLocked == false)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;

                transform.position = touchPosition;

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = touchPosition;
                }
            }
        }
        else
        {
            touchLocked = true;
        }
    }
}

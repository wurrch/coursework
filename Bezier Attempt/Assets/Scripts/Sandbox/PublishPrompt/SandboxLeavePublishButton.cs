using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxLeavePublishButton : MonoBehaviour
{
    public GameObject objectToShow;
    public GameObject objectToHide;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            if (IsTapOnObject()){
                objectToHide.SetActive(false);
                objectToShow.SetActive(true);
            }
        }
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

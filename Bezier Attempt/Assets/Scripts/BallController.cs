using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color unselectColour;
    Color selectColour;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        unselectColour = spriteRenderer.color;
        selectColour = spriteRenderer.color;
        selectColour.a = 0.5f;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (IsTapOnObject()){
                spriteRenderer.color = selectColour;
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            if (IsTapOnObject()){
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPos.z = 0f;
                transform.position = touchPos;
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            spriteRenderer.color = unselectColour;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishpoint : MonoBehaviour
{
    bool isButtonActive = false;

    public void DisableEditMode(){
        gameObject.GetComponent<ButtonColourChange>().enabled = false;
    }

    public void EnableEditMode(){
        gameObject.GetComponent<ButtonColourChange>().enabled = true;
    }


    void Update(){
        if (GameObject.Find("PlayButton").GetComponent<SandboxPlay>().sandboxCurrentlyPlaying == false){
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (IsTapOnObject()){
                    isButtonActive = true;

                    if (GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode == true){
                        Destroy(gameObject);
                        GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode = false;
                    }
                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
                isButtonActive = false;
            }

            if (isButtonActive){
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPos.z = 0f;
                transform.position = touchPos;
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

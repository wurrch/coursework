using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePrompt : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if (!IsTapOnObject()){
                if (GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode == true){
                    GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode = false;
                    gameObject.SetActive(false);
                }
            }
        }
        if (GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode == false){
            gameObject.SetActive(false);
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

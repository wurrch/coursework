using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public GameObject deletePrompt;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if (IsTapOnObject()){
                if (GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode == true){
                    GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode = false;
                    deletePrompt.SetActive(false);
                }
                else {
                    GameObject.Find("GlobalObject").GetComponent<GlobalScript>().deleteMode = true;
                    deletePrompt.SetActive(true);
                }

                GameObject.Find("ToolboxButton").GetComponent<ToolboxButton>().CloseList();
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

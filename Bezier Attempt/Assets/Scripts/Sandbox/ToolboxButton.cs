using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxButton : MonoBehaviour
{
    public bool listOpen = false;
    public GameObject toolboxObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if (IsTapOnObject()){
                if (listOpen == false){
                    OpenList();
                }
                else{
                    CloseList();
                }
            }
        }
    }

    void OpenList(){
        listOpen = true;
        toolboxObject.SetActive(true);
    }
    public void CloseList(){
        listOpen = false;
        toolboxObject.SetActive(false);

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

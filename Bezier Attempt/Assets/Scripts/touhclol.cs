using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touhclol : MonoBehaviour
{
    public GameObject lolObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0f;
            transform.position = touchPos;
            print(touchPos);
            Instantiate(lolObject, touchPos, Quaternion.identity);
        }
    }
}

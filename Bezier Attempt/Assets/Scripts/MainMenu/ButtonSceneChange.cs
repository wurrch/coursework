using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{
    public string nextScene;
    public bool isBallActive = false;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            if (IsTapOnObject()){
                GameObject.Find("GlobalObject").GetComponent<GlobalScript>().currentScene = nextScene;
                if (nextScene == "LevelSelect"){
                    GameObject.Find("GlobalObject").GetComponent<GlobalScript>().isLoadedLevel = false;
                }

                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
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

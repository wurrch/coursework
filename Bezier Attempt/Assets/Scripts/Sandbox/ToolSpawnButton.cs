using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSpawnButton : MonoBehaviour
{
    public GameObject toolPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if (IsTapOnObject()){
                if(gameObject.name == "SpawnpointButton"){
                    Destroy(GameObject.FindGameObjectWithTag("CharacterSpawnpoint"));
                    GameObject.Instantiate(toolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else if (gameObject.name == "FinishpointButton"){
                    Destroy(GameObject.FindGameObjectWithTag("Finishpoint"));
                    GameObject.Instantiate(toolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else if (gameObject.name == "CurveButton"){
                    GameObject curveObject = GameObject.Instantiate(toolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    curveObject.GetComponent<BezierCollider2D>().SandboxSpawn();
                }
                else if (gameObject.name == "BoosterButton"){
                    GameObject booster = GameObject.Instantiate(toolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    booster.GetComponent<Booster>().SandboxSpawn();
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

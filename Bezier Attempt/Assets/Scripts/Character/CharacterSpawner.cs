using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterBallPrefab;
    public GameObject characterBall;
    bool isBallActive = false;

    public void SpawnCharacterBall(){
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        characterBall = GameObject.Instantiate(characterBallPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
    }

    void Update(){
        if (GameObject.Find("PlayButton").GetComponent<SandboxPlay>().sandboxCurrentlyPlaying == false){
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (IsTapOnObject()){
                    isBallActive = true;
                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
                isBallActive = false;
            }

            if (isBallActive){
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

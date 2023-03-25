using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public bool isBallActive = false;
    public bool selectedMode = true;
    public Vector2 forceVector;

    public Vector2 firstPoint = Vector2.zero;
    public Vector2 boosterPoint = Vector2.zero;
    public Vector2 prevFirstPoint = Vector2.zero;
    public Vector2 prevBoosterPoint = Vector2.zero;

    public GameObject controlPrefab;
    GameObject firstControlBall;

    public void Start()
    {
        firstControlBall = GameObject.Instantiate(controlPrefab, new Vector3(1, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        firstPoint = new Vector2(firstControlBall.transform.position.x, firstControlBall.transform.position.y);
        boosterPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if (prevFirstPoint != firstPoint ||
        prevBoosterPoint != boosterPoint) {
            firstPoint = firstControlBall.transform.position;
            boosterPoint = gameObject.transform.position;
            prevFirstPoint = firstPoint;
            prevBoosterPoint = boosterPoint;

            Vector3 vectorDiff = firstControlBall.transform.position - gameObject.transform.position;
            gameObject.transform.up = vectorDiff;
            forceVector = vectorDiff;
        }

        if (GameObject.Find("PlayButton").GetComponent<SandboxPlay>().sandboxCurrentlyPlaying == false) {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (IsTapOnObject() ||
                firstControlBall.GetComponent<BallController>().IsTapOnObject()) {
                    selectedMode = true;
                    firstControlBall.SetActive(true);
                }
                else {
                    selectedMode = false;
                    firstControlBall.SetActive(false);
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (IsTapOnObject()) {
                    isBallActive = true;
                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
                isBallActive = false;
            }

            if (isBallActive) {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPos.z = 0f;
                transform.position = touchPos;
            }
        }
        else{
            selectedMode = false;
            firstControlBall.SetActive(false);
        }
    }

    public bool IsTapOnObject(){
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject){
            return true;
        }
        return false;
    }
}

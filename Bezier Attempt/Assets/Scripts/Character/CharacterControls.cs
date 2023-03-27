using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    private bool isColliding;
    Rigidbody2D rigidbody;
    CircleCollider2D collider;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isColliding){
            if(Input.GetKey(KeyCode.RightArrow)){
                rigidbody.AddForce(new Vector2(8f, 0), ForceMode2D.Force);
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                rigidbody.AddForce(new Vector2(-8f, 0), ForceMode2D.Force);
            }

            if (Input.touchCount > 0){
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                if(touchPos.x > 0){
                    rigidbody.AddForce(new Vector2(5f, 0), ForceMode2D.Force);
                }
                else if(touchPos.x < 0){
                    rigidbody.AddForce(new Vector2(-5f, 0), ForceMode2D.Force);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        isColliding = true;
    }

    void OnTriggerStay2D(Collider2D collider){
        if (collider.GetComponent<Collider2D>().tag == "Booster"){
            rigidbody.AddForce(collider.GetComponent<Booster>().forceVector * 20, ForceMode2D.Force);
        }
        else if (collider.GetComponent<Collider2D>().tag == "Finishpoint"){
            GameObject.Find("PlayButton").GetComponent<SandboxPlay>().SandboxStopPlay();
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        isColliding = false;
    }

    void OnBecameInvisible(){
        GameObject.Find("PlayButton").GetComponent<SandboxPlay>().SandboxStopPlay();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    private bool isTouching;
    Rigidbody2D rigidbody;
    CircleCollider2D collider;
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

    }
    // Update is called once per frame
    void Update()
    {
        if(isTouching){
            if(Input.GetKey(KeyCode.RightArrow)){
                rigidbody.AddForce(new Vector2(5f, 0), ForceMode2D.Force);
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                rigidbody.AddForce(new Vector2(-5f, 0), ForceMode2D.Force);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        isTouching = true;
    }

    void OnCollisionExit2D(Collision2D collision){
        isTouching = false;
    }
}

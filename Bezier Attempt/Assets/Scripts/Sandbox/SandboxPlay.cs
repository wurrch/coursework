using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxPlay : MonoBehaviour
{
    public bool sandboxCurrentlyPlaying = false;
    public Sprite startSprite;
    public Sprite stopSprite;

    void Update(){
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if (IsTapOnObject()){
                if (sandboxCurrentlyPlaying == false){
                    SandboxBeginPlay();
                }
                else if(sandboxCurrentlyPlaying == true){

                }
            }
        }
    }
   // Update is called once per frame
    void SandboxBeginPlay()
    {
        try{
            gameObject.GetComponent<SpriteRenderer>().sprite = stopSprite;
            GameObject.Find("ToolboxButton").GetComponent<ToolboxButton>().CloseList();
            GameObject.Find("ToolboxButton").SetActive(false);
            GameObject.Find("CharacterSpawner").GetComponent<CharacterSpawner>().SpawnCharacterBall();
        }
        catch{
            print("Character spawner with a script cannot be found");
        }

        sandboxCurrentlyPlaying = true;
    }

    void SandboxStopPlay(){
        try{
            gameObject.GetComponent<SpriteRenderer>().sprite = startSprite;
        }
        catch{

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

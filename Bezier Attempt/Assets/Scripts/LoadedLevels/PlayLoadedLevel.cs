using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoadedLevel : MonoBehaviour
{
    public bool loadedLevelCurrentlyPlaying = false;
    public Sprite startSprite;
    public Sprite stopSprite;

    void Update(){
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if (IsTapOnObject()){
                if (loadedLevelCurrentlyPlaying == false){
                    LoadedLevelBeginPlay();
                }
                else if(loadedLevelCurrentlyPlaying == true){
                    LoadedLevelStopPlay();
                }
            }
        }
    }
    // Update is called once per frame
    void LoadedLevelBeginPlay()
    {
        try{
            gameObject.GetComponent<SpriteRenderer>().sprite = stopSprite;
            GameObject.FindGameObjectWithTag("CharacterSpawnpoint").GetComponent<CharacterSpawner>().SpawnCharacterBall();
        }
        catch{
            print("Character spawner with a script cannot be found");
        }

        loadedLevelCurrentlyPlaying = true;
    }

    public void LoadedLevelStopPlay(){
        try{
            gameObject.GetComponent<SpriteRenderer>().sprite = startSprite;
            Destroy(GameObject.FindGameObjectWithTag("CharacterBall"));
        }
        catch{
            print("Couldn't stop the game");
        }

        loadedLevelCurrentlyPlaying = false;
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

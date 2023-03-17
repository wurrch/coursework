using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterBallPrefab;
    // Update is called once per frame
    void SpawnCharacterBall(){
        characterBall = GameObject.Instantiate(characterBallPrefab, new Vector3(firstPoint.x, firstPoint.y, 0), Quaternion.identity);
    }
}

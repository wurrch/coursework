using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public string currentScene = "MainMenu";
    public bool isLoadedLevel = false;
    public bool deleteMode = false;

    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GlobalObject");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        Application.targetFrameRate = 60;

        DontDestroyOnLoad(this.gameObject);
    }
}

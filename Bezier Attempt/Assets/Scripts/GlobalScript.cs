using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public string hello = "first";
    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GlobalObject");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}

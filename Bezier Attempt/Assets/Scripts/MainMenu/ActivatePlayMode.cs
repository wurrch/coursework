using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ActivatePlayMode : MonoBehaviour
{
    public GameObject disabledFrame;
    public GameObject enabledFrame;

    void Start() {
        Directory.CreateDirectory(Application.persistentDataPath + "/SavedLevels");
        string[] files = Directory.GetFiles(Application.persistentDataPath + "/SavedLevels");

        if (files.Length > 0) {
            disabledFrame.SetActive(false);
            enabledFrame.SetActive(true);
        }
        else {
            enabledFrame.SetActive(false);
            disabledFrame.SetActive(true);
        }
    }
}

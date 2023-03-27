using System.IO;
using TMPro;
using UnityEngine;

public class LoadLevelList : MonoBehaviour {
    public GameObject buttonPrefab;
    public GameObject buttonInHireacy;

    public void Start() {
        string[] files = Directory.GetFiles(Application.persistentDataPath + "/SavedLevels");

        foreach (string file in files) {
            GameObject levelSlot = GameObject.Instantiate(buttonPrefab);
            levelSlot.SetActive(true);

            string levelName = file.Substring(file.LastIndexOf("/") + 1);
            levelSlot.GetComponentInChildren<TMP_Text>().SetText(levelName.Remove(levelName.Length - 5));

            levelSlot.transform.SetParent(buttonInHireacy.transform.parent, false);
        }
    }
}

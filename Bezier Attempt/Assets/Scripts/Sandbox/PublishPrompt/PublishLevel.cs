using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData{
    public Vector2 spawnpointPos;
    public Vector2 finishPos;

    [System.Serializable]
    public struct CurveStruct{
        public Vector2 firstControlPoint;
        public Vector2 firstHandlePoint;
        public Vector2 lastControlPoint;
        public Vector2 lastHandlePoint;
    }
    public List<CurveStruct> curveList = new List<CurveStruct>();

    [System.Serializable]
    public struct BoosterStruct {
        public Vector2 boosterPos;
        public Vector2 controlPoint;
    }
    public List<BoosterStruct> boosterList = new List<BoosterStruct>();
}

public class PublishLevel : MonoBehaviour {
    private LevelData data = new LevelData();
    private TouchScreenKeyboard fileNameKeyboard;
    private string userInput = "";

    void SaveFile(){
        PopulateData(data);

        string jsonString = JsonUtility.ToJson(data);
        print(jsonString);

        Directory.CreateDirectory(Application.persistentDataPath + "/SavedLevels");
        File.WriteAllText(Application.persistentDataPath + "/SavedLevels/" + fileNameKeyboard.text, jsonString);
        print("Yes");
    }

    void PopulateData(LevelData data){
        try {
            data.spawnpointPos = GameObject.FindGameObjectWithTag("CharacterSpawnpoint").transform.position;
            data.finishPos = GameObject.FindGameObjectWithTag("Finishpoint").transform.position;
        }
        catch {
            print("No start or finish");
        }

        GameObject[] curves = GameObject.FindGameObjectsWithTag("CurveObject");
        foreach (GameObject curve in curves){
            LevelData.CurveStruct curvePointStruct = new LevelData.CurveStruct();
            curvePointStruct.firstControlPoint = curve.GetComponent<BezierCollider2D>().firstPoint;
            curvePointStruct.firstHandlePoint = curve.GetComponent<BezierCollider2D>().firstHandle;
            curvePointStruct.lastControlPoint = curve.GetComponent<BezierCollider2D>().lastPoint;
            curvePointStruct.lastHandlePoint = curve.GetComponent<BezierCollider2D>().lastHandle;

            data.curveList.Add(curvePointStruct);
        }

        GameObject[] boosters = GameObject.FindGameObjectsWithTag("Booster");
        foreach (GameObject booster in boosters){
            LevelData.BoosterStruct boosterPointStruct = new LevelData.BoosterStruct();
            boosterPointStruct.boosterPos = booster.GetComponent<Booster>().boosterPoint;
            boosterPointStruct.controlPoint = booster.GetComponent<Booster>().firstPoint;

            data.boosterList.Add(boosterPointStruct);
        }
    }

    void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            if (IsTapOnObject()) {
                TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "File Name", 0);
                TouchScreenKeyboard.Open("hello");
//                if (fileNameKeyboard.status == TouchScreenKeyboard.Status.Done){
//                    userInput = fileNameKeyboard.text;
//                }

                //SaveFile();

                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
    }

    bool IsTapOnObject() {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject) {
            return true;
        }
        return false;
    }
}

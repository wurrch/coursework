using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData{
    public Vector2 spawnpointPos = new Vector2(69420, 0);
    public Vector2 finishPos = new Vector2(69420, 0);

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
    public TMP_InputField inputField;
    public GameObject invalidNameText;

    void Start() {
        inputField.onValueChanged.AddListener(delegate {UpdateInputText(); });
    }

    void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            if (IsTapOnObject()) {
                if (CheckForValidName()){
                    SaveFile();
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                }
                else{
                    invalidNameText.SetActive(true);
                }
            }
        }
    }

    void UpdateInputText(){
        if (CheckForValidName()){
            invalidNameText.SetActive(false);
        }
        else{
            invalidNameText.SetActive(true);
        }
    }

    bool CheckForValidName(){
        string[] currentFiles = Directory.GetFiles(Application.persistentDataPath + "/SavedLevels");

        foreach (string file in currentFiles){
            if (file == (Application.persistentDataPath + "/SavedLevels/" + inputField.text + ".json")){
                return false;
            }
        }

        return true;
    }

    void SaveFile(){
        PopulateData();

        string jsonString = JsonUtility.ToJson(data);
        print(jsonString);

        File.WriteAllText(Application.persistentDataPath + "/SavedLevels/" + inputField.text + ".json", jsonString);
    }

    void PopulateData(){
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

    bool IsTapOnObject() {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject) {
            return true;
        }
        return false;
    }
}

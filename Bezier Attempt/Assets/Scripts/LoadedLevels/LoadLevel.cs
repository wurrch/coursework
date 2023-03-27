using System.IO;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public GameObject spawnpointPrefab;
    public GameObject finishpointPrefab;
    public GameObject curvePrefab;
    public GameObject boosterPrefab;

    private LevelData data = new LevelData();

    void Start()
    {
        UnpackLevelFile();
    }

    void UnpackLevelFile() {
        string levelName = GameObject.Find("GlobalObject").GetComponent<GlobalScript>().currentScene;

        string jsonString = File.ReadAllText(Application.persistentDataPath + "/SavedLevels/" + levelName + ".json");
        data = JsonUtility.FromJson<LevelData>(jsonString);

        if (data.spawnpointPos.x != 69420){
            GameObject.Instantiate(spawnpointPrefab, new Vector3(data.spawnpointPos.x, data.spawnpointPos.y, 0), Quaternion.identity);
        }

        if (data.finishPos.x != 69420){
            GameObject.Instantiate(finishpointPrefab, new Vector3(data.finishPos.x, data.finishPos.y, 0), Quaternion.identity);
        }

        foreach (LevelData.CurveStruct curveData in data.curveList){
            GameObject curveObject = GameObject.Instantiate(curvePrefab);
            curveObject.GetComponent<BezierCollider2D>().PlayModeSpawn(curveData);
        }

        foreach (LevelData.BoosterStruct boosterData in data.boosterList){
            GameObject booster = GameObject.Instantiate(boosterPrefab);
            booster.GetComponent<Booster>().PlayModeSpawn(boosterData);
        }
    }
}

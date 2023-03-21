using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelList : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public List<GameObject> buttonList;

    public void Start()
    {
        GameObject newButton = Instantiate(buttonPrefab) as GameObject;
        newButton.transform.SetParent(buttonContainer);
        newButton.GetComponentInChildren<Text>().text = "lol";
        buttonList.Add(newButton);
        newButton.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(newButton));
    }

    public void ButtonClicked(GameObject clickedButton)
    {
        Debug.Log("Button clicked: " + clickedButton.GetComponentInChildren<Text>().text);
    }
}

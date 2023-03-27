using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayModelLevelButton : MonoBehaviour
{
    public Button playButton;
    public TMP_Text textBox;

    void Start()
    {
        playButton.onClick.AddListener(delegate  {loadPlayModeScene();});
    }

    void loadPlayModeScene(){
        GameObject.Find("GlobalObject").GetComponent<GlobalScript>().currentScene = textBox.text;
        GameObject.Find("GlobalObject").GetComponent<GlobalScript>().isLoadedLevel = true;
        SceneManager.LoadScene("LoadedLevel", LoadSceneMode.Single);
    }
}

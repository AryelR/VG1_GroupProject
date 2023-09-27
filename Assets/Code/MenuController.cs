using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    string sceneToLoad;

    void LoadScene() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevelMap() {
        sceneToLoad = "Level Map";
        LoadScene();
    }

    public void LoadLevel1() {
        sceneToLoad = "Level 1";
        LoadScene();
    }

    public void Quit() {
        Application.Quit();
    }
}

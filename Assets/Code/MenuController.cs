using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    string sceneToLoad;
    public static int current_level = 1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void LoadScene() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevelMap() {
        sceneToLoad = "Level Map";
        LoadScene();
    }
    public void loadLevel(int level)
    {
        Debug.Log(current_level);
        if(current_level >= level)
        {
            sceneToLoad = "Level " + level;
            LoadScene();
        }
        
    }

    public void Quit() {
        Application.Quit();
    }
}

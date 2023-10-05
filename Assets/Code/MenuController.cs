using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public GameObject Lock2;
    public GameObject Lock3;
    public GameObject Lock4;
    public GameObject Lock5;
    public GameObject Lock6;

    string sceneToLoad;
    public static int current_level = 1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Lock2.SetActive(true);
        Lock3.SetActive(true);
        Lock4.SetActive(true);
        Lock5.SetActive(true);
        Lock6.SetActive(true);
        if (current_level > 1)
        {
            Lock2.SetActive(false);
        }
        if(current_level > 2)
        {
            Lock3.SetActive(false);
        }
        if (current_level > 3)
        {
            Lock4.SetActive(false);
        }
        if (current_level > 4)
        {
            Lock5.SetActive(false);
        }
        if(current_level > 5)
        { 
            Lock6.SetActive(false); 
        }
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

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditorInternal;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class LevelController : MonoBehaviour
{
    //variables
    
    public string levelName;
    bool selected;
    public int ID;
    public static List<int> Levels = new List<int>();
    public static Dictionary<int, bool> completedLevels = new Dictionary<int, bool>();

    private void Awake()
    {
        //print completed Levels
        foreach (var value in completedLevels.Values)
        {
            Debug.Log(value);
        }

    }
    void Start()
    {
        if (!Levels.Contains(ID))
        {
            Levels.Add(ID);
        }
        if (!completedLevels.ContainsKey(ID))
        {
            completedLevels[ID] = false;
        }
        selected = false;
        //hide popup
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        Canvas canvas = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Canvas>();
        //get completed image
        Image image = canvas.transform.GetChild(2).GetComponent<Image>();

        string current_level = "Level " + MenuController.current_level.ToString();
        bool set_active = false;

        set_active = completedLevels[ID];
        if (set_active)
        {

            image.gameObject.SetActive(true);
        }
        else
        {
            image.gameObject.SetActive(false);
        }

        

    }

    void Update()
    {
        //when player is touching level
        if (selected)
        {
            //if enter is pressed, enter level
            if (Input.GetKey(KeyCode.Space))
            {
                //save player's last position
                TopDownPlayerController.lastPosition = gameObject.transform.position;
                //load selected scene
                PlayerController.level = ID;
                SceneManager.LoadScene(levelName);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //check if colliding with player      
        if (other.gameObject.GetComponent<TopDownPlayerController>() || other.gameObject.GetComponent<PlayerController>())
        {    
            //set colliding variable so that if enter is pressed, new level is loaded
            selected = true;
            //level info popup
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //check if not colliding with player      
        if (other.gameObject.GetComponent<TopDownPlayerController>() || other.gameObject.GetComponent<PlayerController>())
        {
            //level no longer selected
            selected = false;
            //hide popup if level not selected
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

}

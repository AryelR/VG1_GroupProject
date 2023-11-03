using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        gameManager.Levels.Add(gameObject);
    }
    void Start()
    {
        for(int i = 0; i < gameManager.Levels.Count; i++)
        {
            Debug.Log(gameManager.Levels[i].name);
        }
        selected = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        Canvas canvas = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Canvas>();
        Image image = canvas.transform.GetChild(2).GetComponent<Image>();
        if(image != null )
        {
            if (gameManager.completedLevels.Contains(gameObject))
            {

                image.gameObject.SetActive(true);
            }
            else
            {
                image.gameObject.SetActive(false);
            }
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
                //load selected scene
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

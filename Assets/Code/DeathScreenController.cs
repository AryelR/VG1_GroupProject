using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setButtonsInteractable()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void Restart()
    {
        //revert speed 
        Time.timeScale = 1.0f;
        //reset timer
        DeathOverlayController.seconds = 0;
        //reload the starting map
        //reset upgrades and health
        GameObject.Find("Player").GetComponent<PlayerController>().Reset();
        SceneManager.LoadScene("Level Map");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

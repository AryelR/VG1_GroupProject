using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Restart()
    {
        //revert speed 
        Time.timeScale = 1.0f;
        //reset timer
        DeathOverlayController.seconds = 0;
        //reload the starting map
        SceneManager.LoadScene("Level Map");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

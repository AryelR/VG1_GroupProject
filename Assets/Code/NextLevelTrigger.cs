using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            /*
            if (SceneManager.GetActiveScene().name.Equals("Level " + MenuController.current_level.ToString()))
            {
                MenuController.current_level++;
            }
            */
            string current_level = "Level " + MenuController.current_level.ToString();
            for(int i = 0; i < gameManager.Levels.Count; i++)
            {
                
                if (gameManager.Levels[i] != null)
                {
                    GameObject Level = gameManager.Levels[i];
                    Debug.Log(Level);
                    if (Level.name.Equals(current_level))
                    {
                        gameManager.completedLevels.Add(Level);

                    }
                }
                
            }
            
            for(int i = 0; i < gameManager.completedLevels.Count;i++)
            {
                Debug.Log(gameManager.completedLevels[i].name);
            }
            Debug.Log(MenuController.current_level);
            SceneManager.LoadScene("PowerUp");
        }
    }
}

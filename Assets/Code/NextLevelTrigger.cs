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
            int currentLevel = PlayerController.level;
            bool isLevelCompleted = LevelController.completedLevels[currentLevel];

            if (LevelController.completedLevels.ContainsKey(currentLevel))
            {
                LevelController.completedLevels[currentLevel] = true;
                if((!isLevelCompleted && LevelController.completedLevels[currentLevel]))
                {
                    levelsCompleted.completedLevel();
                }
                
            }

            bool allCompleted = true;
            foreach(var value in LevelController.completedLevels.Values) 
            {
                allCompleted = allCompleted && value;
            }
            if (allCompleted)
            {
                SceneManager.LoadScene("Win");
            }
            else if(SceneManager.GetActiveScene().name == "Tutorial Level" || !(!isLevelCompleted && LevelController.completedLevels[currentLevel])) {
                SceneManager.LoadScene("Level Map");
            }
            else
            {
                SceneManager.LoadScene("PowerUp");
            }
        }
    }
}

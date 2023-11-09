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
            if (LevelController.completedLevels.ContainsKey(currentLevel))
            {
                LevelController.completedLevels[currentLevel] = true;
            }
            SceneManager.LoadScene("PowerUp");
        }
    }
}

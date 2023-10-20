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
            Debug.Log(MenuController.current_level);
            SceneManager.LoadScene("PowerUp");
        }
    }
}

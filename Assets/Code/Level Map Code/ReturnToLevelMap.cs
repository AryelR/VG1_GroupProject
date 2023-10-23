using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLevelMap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //check if colliding with player, load level map     
        if (other.gameObject.GetComponent<TopDownPlayerController>() || other.gameObject.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene("Level Map");
        }
    }
}

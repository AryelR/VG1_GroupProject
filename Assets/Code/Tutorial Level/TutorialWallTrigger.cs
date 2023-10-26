using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWallTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //check if colliding with player
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            //show hidden
            GameObject.Find("HiddenUntilTrigger").SetActive(true);
        }
    }
}

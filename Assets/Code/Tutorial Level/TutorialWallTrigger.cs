using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWallTrigger : MonoBehaviour
{
    //variables
    public GameObject hidden;
    void OnTriggerEnter2D(Collider2D other)
    {
        //check if colliding with player
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            //show hidden
            hidden.SetActive(true);
        }
    }
}

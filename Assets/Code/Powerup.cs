using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public string type;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            Destroy(gameObject);
            if(type == "jump")
            {
                PlayerController.jumpForce += 3f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ForegroundHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        //check if colliding with player
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            Debug.Log("hi");
            gameObject.GetComponent<Tilemap>().color = new Color(1, 1, 1, .25f);
            
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        //check if colliding with player
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            gameObject.GetComponent<Tilemap>().color = new Color(1, 1, 1, 1);

        }
    }
    
}

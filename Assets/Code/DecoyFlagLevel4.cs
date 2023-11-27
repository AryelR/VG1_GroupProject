using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyFlagLevel4 : MonoBehaviour
{
    public static Boolean touched;
    private GameObject player;
    public GameObject wall;
    private Vector2 teleportPos = new Vector2(7.7f, 20.3f);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        touched = true;
        player.transform.position = teleportPos;
        wall.transform.position = new Vector2(teleportPos.x - 20, wall.transform.position.y);

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        touched = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyFlagLevel4 : MonoBehaviour
{
    public static Boolean touched;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        touched = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        touched = false;
    }
}

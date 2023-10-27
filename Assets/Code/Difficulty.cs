using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public enum difficulty { easy, medium, hard};
    public difficulty choice;
    public Image this_image;
    public Sprite easy;
    public Sprite medium;
    public Sprite hard;

    // Start is called before the first frame update
    void Start()
    {
        

        if(choice == difficulty.easy)
        {
            this_image.sprite = easy;
        }
        if(choice == difficulty.medium)
        {
            this_image.sprite = medium;
        }
        if(choice == difficulty.hard) 
        { 
            this_image.sprite = hard;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

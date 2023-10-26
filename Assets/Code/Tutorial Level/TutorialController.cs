using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject hidden;
    // Start is called before the first frame update
    void Start()
    {
        //hide wall and text
        GameObject.Find("HiddenUntilTrigger").SetActive(false);
    }

}

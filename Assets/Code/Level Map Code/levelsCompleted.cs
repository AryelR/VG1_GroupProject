using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelsCompleted : MonoBehaviour
{
    public static int numberOfLevels = 0;
    public static int levelCompletednumber = 0;
    public static TMP_Text levelsInfo;
    // Start is called before the first frame update
    void Start()
    {
        levelsInfo = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public static void updatestuff(int numLevels)
    {
        numberOfLevels = numLevels;
        string info = "Levels Completed: ";
        info += levelCompletednumber.ToString();
        info += "/";
        info += numberOfLevels.ToString();
        //if(levelsInfo != null ) {
            levelsInfo.text = info;
        //}
        
    }

    public static void completedLevel()
    {
        levelCompletednumber++;
    }


}

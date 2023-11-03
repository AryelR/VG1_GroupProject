using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static List<GameObject> Levels = new List<GameObject>();
    public static List<GameObject> completedLevels = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

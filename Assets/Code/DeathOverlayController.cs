using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class DeathOverlayController : MonoBehaviour
{
    public GameObject deathOverlayPrefab;
    public static float seconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
    }

    public void showOverlay()
    {
        GameObject overlay = Instantiate(deathOverlayPrefab, gameObject.transform);
        overlay.transform.Find("Text (TMP) - Jump Count").GetComponent<TextMeshProUGUI>().text = PlayerController.totalJumps.ToString();
        overlay.transform.Find("Text (TMP) - Time Count").GetComponent<TextMeshProUGUI>().text = secondsToFormattedTime(seconds);
    }

    public String secondsToFormattedTime(float numSeconds)
    {
        String hours = TimeSpan.FromSeconds(numSeconds).Hours.ToString("d2");
        String minutes = TimeSpan.FromSeconds(numSeconds).Minutes.ToString("d2");
        String formatSeconds = TimeSpan.FromSeconds(numSeconds).Seconds.ToString("d2");
        return hours + ":" + minutes + ":" + formatSeconds;
    }
}

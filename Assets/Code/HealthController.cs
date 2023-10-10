using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject heartPrefab;
    public GameObject halfHeartPrefab;
    public GameObject brokenHeartPrefab;

    public void DrawHearts(double currentHealth, double maxHealth)
    {
        double healthTracker = currentHealth;

        //help from https://youtu.be/D6SeFFKZOFc   
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            //display a half heart when last bit of health needs to be shown
            if (healthTracker == 0.5)
            {
                GameObject heart = Instantiate(halfHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
                heart.transform.localScale = new Vector3(1, 1, 1);
                healthTracker -= 0.5;
            }
            //display a full heart otherwise
            else if (healthTracker > 0)
            {
                GameObject heart = Instantiate(heartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
                heart.transform.localScale = new Vector3(1, 1, 1);
                healthTracker -= 1;
            }
            //display an empty heart if no more hearts
            else
            {
                GameObject heart = Instantiate(brokenHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
                heart.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void SetFlashing()
    {
        foreach (Transform child in transform)
        {
            //only flash full or half hearts
            if (!child.name.Contains("Empty"))
            {
                child.gameObject.GetComponent<Animator>().SetBool("Flashing", true);
            }
            
        }
    }

    public void StopFlashing()
    {
        foreach (Transform child in transform)
        {
            if (!child.name.Contains("Empty"))
            {
                child.gameObject.GetComponent<Animator>().SetBool("Flashing", false);
            }
        }
    }
}

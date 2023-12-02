using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class powerUpController : MonoBehaviour
{
    public GameObject[] power_ups;
    public string[] names;
    public GameObject CanvasObject;
    public GameObject textMeshProPrefab; // Reference to your TextMeshPro prefab

    // Start is called before the first frame update
    void Start()
    {
        if (power_ups.Length > 1)
        {
            int random1 = Random.Range(0, power_ups.Length);
            int random2 = Random.Range(0, power_ups.Length);
            while (random1 == random2)
            {
                random2 = Random.Range(0, power_ups.Length);
            }
            Debug.Log(random1 + " " + random2);
            Debug.Log(power_ups[random1].name);
            Debug.Log(power_ups[random2].name);

            Debug.Log("HERE");
            GameObject powerup1 = Instantiate(power_ups[random1], new Vector3(-100, 75, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            powerup1.transform.SetParent(CanvasObject.transform, false);
            powerup1.SetActive(true);

            // Instantiate TextMeshPro object below powerup1
            GameObject powerUp1Text = Instantiate(textMeshProPrefab, powerup1.transform.position - new Vector3(300, 300, 0), Quaternion.identity);
            powerUp1Text.transform.SetParent(CanvasObject.transform, false);
            powerUp1Text.GetComponent<TextMeshProUGUI>().text = names[random1];
            powerUp1Text.SetActive(true);

            GameObject powerup2 = Instantiate(power_ups[random2], new Vector3(100, 75, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            powerup2.transform.SetParent(CanvasObject.transform, false);
            powerup2.SetActive(true);
            Debug.Log("HERE2");

            // Instantiate TextMeshPro object below powerup2
            GameObject powerUp2Text = Instantiate(textMeshProPrefab, powerup2.transform.position - new Vector3(300, 300, 0), Quaternion.identity);
            powerUp2Text.transform.SetParent(CanvasObject.transform, false);
            powerUp2Text.GetComponent<TextMeshProUGUI>().text = names[random2];
            powerUp2Text.SetActive(true);
        }
        else
        {
            Debug.Log("Please use more than 1 power up");
        }
    }
}

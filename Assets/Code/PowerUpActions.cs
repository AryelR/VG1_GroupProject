using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PowerUpActions : MonoBehaviour
{
    public void jump_boost()
    {
        PlayerController.jumpForce += 3f;
        SceneManager.LoadScene("Level Map");
    }

    public void speed_boost()
    {
        PlayerController.movementSpeed += 3f;
        SceneManager.LoadScene("Level Map");
    }
}

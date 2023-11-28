using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PowerUpActions : MonoBehaviour
{
    public void jump_boost()
    {
        PlayerController.jumpForce += 1f;
        SceneManager.LoadScene("Level Map");
    }

    public void speed_boost()
    {
        PlayerController.movementSpeed += 1f;
        SceneManager.LoadScene("Level Map");
    }

    public void gravity_boost()
    {
        PlayerController.gravForce += 100f;
        SceneManager.LoadScene("Level Map");
    }
}

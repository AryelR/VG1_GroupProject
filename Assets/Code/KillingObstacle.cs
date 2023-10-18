using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillingObstacle : MonoBehaviour
{
    //config
    public double damage = 0.5;

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if colliding with player who is not invincible or already hit
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player && !(player.isInvincible || player.isHit))
        {
            PlayerController.health -= damage;
            //kill or hurt player
            player.Die();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

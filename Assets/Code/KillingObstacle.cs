using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillingObstacle : MonoBehaviour
{
    //outlet
    private Animator animator;

    //config
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if colliding with player who is not invincible or already hit
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player && !(player.isInvincible || player.isHit))
        {
            player.health -= damage;
            //kill player
            player.Die();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

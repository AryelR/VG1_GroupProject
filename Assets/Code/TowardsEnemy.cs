using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsEnemy : MonoBehaviour
{
    public float speed = 5f;
    private Transform player;

    void Start()
    {
        // Find the player GameObject using its tag ("Player").
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // Move towards the player.
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player.
            Vector3 direction = player.position - transform.position;

            // Normalize the direction vector to get a unit vector.
            direction.Normalize();

            // Move the enemy towards the player.
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a specific tag or layer if needed
        // For example, destroy the object only if it collides with an object tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}

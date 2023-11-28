using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    // Directly use the correct field type
    public List<Rigidbody2D> birds;
    public Rigidbody2D _rb;
    //public GameObject bird;

    // Make this field adjustable via the Inspector for fine tuning
    //[SerializeField] private float gravitationalForce = 5;

    // Your start was redundant
    // 

    void Start()
    {
        GameObject b = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D birdRigid = b.GetComponent<Rigidbody2D>();
        birds[0] = (birdRigid);

        _rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // iterate through all birds
        // if(Input.GetKeyDown(KeyCode.Q))
        // {
            foreach (var bird in birds)
            {
                //if (Input.GetKeyDown(KeyCode.Q))
                //{
                var directionOfBirdFromPlanet = ((Vector2)transform.position - bird.position).normalized;

                // Adds the force towards the center

                bird.AddForce(directionOfBirdFromPlanet * PlayerController.gravForce);
                // }

            }
        // }

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ceiling" && other != null)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //check if colliding with player who is not invincible or already hit

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingEnemy : MonoBehaviour
{

    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
       _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    // Update is called once per frame
    public float speed; // Adjust the speed as needed
    private float timer = 0.0f;
    private bool movingForward = true;


    public Transform selfObject; // Reference to the object itself
    public Transform otherObject; // Reference to the other object


    public GameObject bullet;
    public Transform shootPoint;

    public float fireRate = 3.0f;

    private float nextFireTime = 0.0f;

    // Update is called once per frame
    void Update()
    {

        if (selfObject != null && otherObject != null)
        {
            float distance = Vector3.Distance(selfObject.position, otherObject.position);
            if (Time.time >= nextFireTime && distance < 20)
            {
                shoot();
                nextFireTime = Time.time + 1.0f / fireRate;
            }
        }



        // Move forward for 5 seconds
        if (movingForward)
        {
            transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
            timer += Time.deltaTime;

            if (timer >= 5.0f)
            {
                movingForward = false;
                timer = 0.0f;
            }
        }
        // Move backward for 5 seconds
        else
        {
            transform.position -= new Vector3(1f * Time.deltaTime, 0, 0);
            timer += Time.deltaTime;

            if (timer >= 5.0f)
            {
                movingForward = true;
                timer = 0.0f;
            }
        }
    }

    void shoot()
    {
        Vector3 SpawnPosition = (transform.position - transform.up);

        Quaternion spawnRotation = transform.rotation;

        GameObject newBullet = Instantiate(bullet, SpawnPosition, spawnRotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = -Vector3.up * 5.0f;
        }
    }
    

}

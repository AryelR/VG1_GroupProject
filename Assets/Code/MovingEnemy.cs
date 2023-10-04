using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingEnemy : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    public float speed; // Adjust the speed as needed
    private float timer = 0.0f;
    private bool movingForward = true;

    void Update()
    {
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
}

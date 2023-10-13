using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{


    public GameObject grapplingHookPrefab;
    public GameObject hook;


    public float hookSpeed = 30f;

    private bool isGrappling = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGrappling)
        {
            ShootHook();
        }
        else if (Input.GetKeyUp(KeyCode.E) && isGrappling)
        {
            // Release the hook
            ReleaseHook();
        }

    }

    void ShootHook()
    {
        // Calculate a vector for a 45-degree angle to the top right
        Vector2 hookDirection = Quaternion.Euler(0, 0, -45) * Vector2.up;

        // Instantiate and shoot the grappling hook in the calculated direction
        hook = Instantiate(grapplingHookPrefab, transform.position, Quaternion.identity);
        Rigidbody2D hookRB = hook.GetComponent<Rigidbody2D>();
        hookRB.velocity = hookDirection.normalized * hookSpeed;

        isGrappling = true;
    }

    void ReleaseHook()
    {
        Destroy(hook);
        isGrappling = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{


    public GameObject grapplingHookPrefab;
    public GameObject hook;


    public float hookSpeed = 30f;

    private bool isGrappling = false;

    public List<Rigidbody2D> birds;
    public Rigidbody2D _rb;

    [SerializeField] private float gravitationalForce = 5;



    // Start is called before the first frame update
    void Start()
    {
        // GameObject b = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D birdRigid = GetComponent<Rigidbody2D>();
        birds[0] = (birdRigid);

        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        hook = Instantiate(grapplingHookPrefab, transform.position + new Vector3(1f, 1f, 0f), Quaternion.identity);
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

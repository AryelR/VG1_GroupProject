using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Outlet
    Rigidbody2D _rigidbody2D;

    //State Tracking
    public int jumpsLeft;

    //config
    public float automaticMovementSpeed;
    public float slowDownSpeed;
    public float speedUpSpeed;
    public float jumpSpeed;

    // Methods
    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Move player automatically to the right
        //_rigidbody2D.AddForce(Vector2.right * automaticMovementSpeed * Time.deltaTime, ForceMode2D.Impulse);

        //Speed player down
        if (Input.GetKey(KeyCode.A)) {
            _rigidbody2D.AddForce(Vector2.left * slowDownSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
        //Speed player up
        if (Input.GetKey(KeyCode.D)) {
            _rigidbody2D.AddForce(Vector2.right * speedUpSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) {
            if (jumpsLeft > 0) {
                jumpsLeft--;
                _rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        //check that we collided with ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            //check what is directly below our character's feet
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);
            //Debug.DrawRay(transform.position, Vector2.down * 0.7f); //visualize raycast

            //We might have multiple things below our character's feet
            for (int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];

                //check that we collided with ground below our feet
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                    //reset jump count
                    jumpsLeft = 1;
                }
            }
        }
    }
}

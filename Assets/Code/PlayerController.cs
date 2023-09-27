using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Outlet
    Rigidbody2D _rigidbody2D;

    //State Tracking
    public int jumpsLeft;
    public float speed;
    public float currentMaxSpeed;

    //config
    public float acceleration;
    public float maxSpeed;
    public float slowDownSpeed;
    public float speedUpSpeed;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    


    // Methods
    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //initialize maxspeed
        currentMaxSpeed = maxSpeed;
    }

    void Update() {     
        //Move player automatically to the right
        _rigidbody2D.AddForce(Vector2.right * acceleration * Time.deltaTime, ForceMode2D.Impulse);

        //No input, reset to base speed 
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            currentMaxSpeed = maxSpeed;
        }

        //Slow player down
        if (Input.GetKeyDown(KeyCode.A)) {
            currentMaxSpeed = slowDownSpeed;
        }
        //Speed player up
        if (Input.GetKeyDown(KeyCode.D)) {
            currentMaxSpeed = speedUpSpeed;
        }

        //jump
        if (Input.GetButtonDown("Jump")) {
            if (jumpsLeft > 0) {
                jumpsLeft--;
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        //when falling, increase gravity
        //if button is not long pressed, do short jump
        //from: https://www.youtube.com/watch?v=7KiK0Aqtmzc
        if (_rigidbody2D.velocity.y < 0) {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        }else if(_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //restrict x velocity based on input, do not restrict y velocity
        _rigidbody2D.velocity = new Vector2(
            Mathf.Clamp(_rigidbody2D.velocity.x, -currentMaxSpeed, currentMaxSpeed),
            _rigidbody2D.velocity.y);

        //set speed
        speed = _rigidbody2D.velocity.magnitude;
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

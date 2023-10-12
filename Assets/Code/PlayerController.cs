using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Outlet
    Rigidbody2D _rigidbody2D;
    public static PlayerController instance;
    private new ParticleSystem particleSystem;
    Animator animator;
    HealthController healthController;

    //State Tracking
    public int jumpsLeft;
    public float speed;
    public float currentMaxSpeed;
    public double health;
    public double maxHealth;
    public bool isHit;
    public bool isInvincible;

    //config
    public float acceleration;
    public float maxSpeed;
    public float slowDownSpeed;
    public float speedUpSpeed;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    void Awake()
    {
        instance = this;
    }

    // Methods
    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        //initialize maxspeed
        currentMaxSpeed = maxSpeed;
        animator = gameObject.GetComponent<Animator>();
        //draw hearts
        healthController = GameObject.Find("HeartContainer").GetComponent<HealthController>();
        healthController.DrawHearts(health, maxHealth);
    }

    
    private void FixedUpdate() {
        //update synced with physics engine
        animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
        if (_rigidbody2D.velocity.magnitude > 0) {
            animator.speed = _rigidbody2D.velocity.magnitude / 3f;
        }
        else {
            animator.speed = 1f;
        }
    }
    

    void Update() {
        //do nothing if hit
        if (isHit) {
            return;
        }
        
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

        //Exit Level
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Level Map");
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

        var main = particleSystem.main;
        main.startSpeed = speed;
    }

    private void OnCollisionStay2D(Collision2D other) {
        //check that we collided with ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            //check what is directly below our character's feet
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.85f);
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

    public void Die() {
        healthController.DrawHearts(health, maxHealth);
        if(health <= 0) {
            //die die
        }
        else {
            animator.SetTrigger("isHit");
        }
    }
    
    //functions called by animator at start of respective anim
    public void setIsHit() {
        isHit = true;
        particleSystem.Pause();
    }

    public void setIsInvincible() {
        animator.ResetTrigger("isHit");
        isInvincible = true;
        isHit = false;
    }

    public void setRun() {
        isInvincible = false;
        isHit = false;
        isHit = false;
        isInvincible = false;
    }
}

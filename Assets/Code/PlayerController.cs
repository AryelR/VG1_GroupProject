using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Drawing;

public class PlayerController : MonoBehaviour {
    //Outlet
    Rigidbody2D _rigidbody2D;
    public static PlayerController instance;
    private new ParticleSystem particleSystem;
    Animator animator;
    HealthController healthController;
    SpriteRenderer sprite;
    DeathOverlayController deathOverlayController;

    //State Tracking
    int jumpsLeft;
    public float speed;
    public float currentMaxSpeed;
    public static double health;
    public static double maxHealth;
    public bool isHit = false;
    public bool isInvincible = false;
    public static bool autorunning = false;
    public static float movementSpeed;
    public static float jumpForce;

    //stat tracking for end of game
    public static float totalJumps = 0;

    //config
    public int jumps;
    public double startingHealth;
    public double startingMaxHealth;
    public float startingMovementSpeed = 12f;
    public float startingJumpForce = 9f;
    public float acceleration;
    public float maxSpeed;
    public float slowDownSpeed;
    public float speedUpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;



    void Awake() {
        instance = this;
    }

    // Methods
    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        //initialize maxspeed
        currentMaxSpeed = maxSpeed;
        animator = gameObject.GetComponent<Animator>();
        //set variables for starting char: health, speed, jump
        if(health == 0) {
            // health is initially initialized to 0, set it to starting health if that's the case
            health = startingHealth;
            maxHealth = startingMaxHealth;
            movementSpeed = startingMovementSpeed;
            jumpForce = startingJumpForce;
        }
        healthController = GameObject.Find("HeartContainer").GetComponent<HealthController>();
        healthController.DrawHearts(health, maxHealth);
        deathOverlayController = GameObject.Find("Death Overlay").GetComponent<DeathOverlayController>();
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

        if (autorunning) {
            Autorun();
        }
        else {
            Run();
        }

        //Exit Level
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Level Map");
        }

        //jump
        if (Input.GetButtonDown("Jump")) {
            if (jumpsLeft > 0) {
                jumpsLeft--;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                totalJumps++;
            }
        }

        //when falling, increase gravity
        //if button is not long pressed, do short jump
        //from: https://www.youtube.com/watch?v=7KiK0Aqtmzc
        if (_rigidbody2D.velocity.y < 0) {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //restrict x velocity based on input, do not restrict y velocity
        _rigidbody2D.velocity = new Vector2(
            Mathf.Clamp(_rigidbody2D.velocity.x, -currentMaxSpeed, currentMaxSpeed),
            _rigidbody2D.velocity.y);

        //set speed tracker
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
                    jumpsLeft = jumps;
                }
            }
        }



        if (other.gameObject.CompareTag("SpeedPowerUp"))
        {
            currentMaxSpeed = 100f;
            acceleration = 100f;
            Destroy(other.gameObject);
        }
    }

    private void Autorun() {

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
    }

    private void Run() {
        //Speed player down
        if (Input.GetKey(KeyCode.A)) {
            sprite.flipX = false;
            _rigidbody2D.AddForce(Vector2.left * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            
        }
        //Speed player up
        if (Input.GetKey(KeyCode.D)) {
            sprite.flipX = true;
            _rigidbody2D.AddForce(Vector2.right * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    public void Die() {
        healthController.DrawHearts(health, maxHealth);
        if (health <= 0) {
            //add death ui to scene 
            deathOverlayController.showOverlay();
            //SceneManager.LoadScene("Death");
            //slow down time and hide player and health ui
            Time.timeScale = 0f;
            sprite.color = new UnityEngine.Color(1f, 1f, 1f, 0f);
            GameObject.Find("HeartContainer").transform.localScale = new Vector3(0, 0, 0);
            //reset upgrades and health
            Reset();
            
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
        particleSystem.Play();
    }

    public void setRun() {
        isInvincible = false;
        isHit = false;
    }

    public void Reset()
    {
        health = startingHealth;
        maxHealth = startingMaxHealth;
        movementSpeed = startingMovementSpeed;
        jumpForce = startingJumpForce;
    }
}

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
    CameraController cameraController;
    public static int level = -1;

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
    public bool isDead = false;
    public static float gravForce;
    public int gravNadesLeft = 3;

    //stat tracking for end of game
    public static float totalJumps = 0;

    //config
    public int jumps;
    public double startingHealth;
    public double startingMaxHealth;
    public float startingMovementSpeed = 15f;
    public float startingJumpForce = 9f;
    public float startingGravForce = 30f;
    public float acceleration;
    public float maxSpeed;
    public float slowDownSpeed;
    public float speedUpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public int gravNum;



    // gravity

    public GameObject grapplingHookPrefab;
    public GameObject hook;

    public float hookSpeed = 20f;


    private bool isGrappling = false;

    public List<Rigidbody2D> birds;



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
            gravForce = startingGravForce;
        }
        healthController = GameObject.Find("HeartContainer").GetComponent<HealthController>();
        healthController.DrawHearts(health, maxHealth);
        deathOverlayController = GameObject.Find("Death Overlay").GetComponent<DeathOverlayController>();
        cameraController = GameObject.Find("Virtual Camera").GetComponent<CameraController>();


        // Gravity
        Rigidbody2D birdRigid = GetComponent<Rigidbody2D>();
        birds[0] = (birdRigid);
        gravForce = startingGravForce;

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
        if (isHit)
        {
            return;
        }

        if (isDead)
        {
            //do nothing
        }
        else if (autorunning) {
            Autorun();
        }
        else {
            Run();
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
        speed = _rigidbody2D.velocity.x;

        var main = particleSystem.main;
        main.startSpeed = speed;


        // gravity grendae
        if (Input.GetKeyDown(KeyCode.E) && gravNum == 0 && gravNadesLeft > 0)
        {
            ShootHookRight();
        }
        else if (Input.GetKeyUp(KeyCode.E) && isGrappling)
        {
            // Release the hook
            ReleaseHook();
        }

        if (Input.GetKeyDown(KeyCode.Q) && gravNum == 0 && gravNadesLeft > 0)
        {
            ShootHookLeft();
        }
        else if (Input.GetKeyUp(KeyCode.Q) && isGrappling)
        {
            // Release the hook
            ReleaseHook();
        }
    }

    void ShootHookRight()
    {
        // Calculate a vector for a 45-degree angle to the top right
        gravNum = 1;
        gravNadesLeft -= 1;
        Vector2 hookDirection = Quaternion.Euler(0, 0, -45) * Vector2.up;



        // Instantiate and shoot the grappling hook in the calculated direction
        hook = Instantiate(grapplingHookPrefab, transform.position + new Vector3(1f, 1f, 0f), Quaternion.identity);
        Rigidbody2D hookRB = hook.GetComponent<Rigidbody2D>();
        hookRB.velocity = hookDirection.normalized * hookSpeed;

        isGrappling = true;
    }

    void ShootHookLeft()
    {
        // Calculate a vector for a 45-degree angle to the top left
        gravNum = 1;
        gravNadesLeft -= 1;
        Vector2 hookDirection = Quaternion.Euler(0, 0, 45) * Vector2.up;



        // Instantiate and shoot the grappling hook in the calculated direction
        hook = Instantiate(grapplingHookPrefab, transform.position + new Vector3(-1f, 1f, 0f), Quaternion.identity);
        Rigidbody2D hookRB = hook.GetComponent<Rigidbody2D>();
        hookRB.velocity = hookDirection.normalized * hookSpeed;

        isGrappling = true;
    }



    void ReleaseHook()
    {
        Destroy(hook);
        isGrappling = false;
        gravNum = 0;
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

        if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            health = startingHealth;
        }
    }

    private void Autorun() {

        //Move player automatically to the right
        _rigidbody2D.AddForce(Vector2.right * acceleration * Time.deltaTime, ForceMode2D.Impulse);

        //No input, reset to base speed 
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            currentMaxSpeed = maxSpeed;
            cameraController.zooming = true;
        }

        //Slow player down
        if (Input.GetKeyDown(KeyCode.A)) {
            currentMaxSpeed = slowDownSpeed;
            cameraController.zooming = true;
        }
        //Speed player up
        if (Input.GetKeyDown(KeyCode.D)) {
            currentMaxSpeed = speedUpSpeed;
            cameraController.zooming = false;
        }
    }

    private void Run() {
        //Speed player up to left
        if (Input.GetKey(KeyCode.A)) {
            sprite.flipX = false;
            _rigidbody2D.AddForce(Vector2.left * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            cameraController.zooming = false;

        }
        //Speed player up to right
        if (Input.GetKey(KeyCode.D)) {
            sprite.flipX = true;
            _rigidbody2D.AddForce(Vector2.right * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            cameraController.zooming = false;
        }
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        {
            cameraController.zooming = true;
        }
    }
    public void Die() {
        healthController.DrawHearts(health, maxHealth);
        cameraController.zooming = true;
        if (health <= 0) {
            StartCoroutine(SlowDownTime(1.0f, 0.01f, 3.0f));
            //set to stop movement and slow down the game
            isDead = true;
            animator.SetTrigger("isHit");
            //add death ui to scene 
            deathOverlayController.showOverlay();
            sprite.color = new UnityEngine.Color(1f, 1f, 1f, 0f);
            GameObject.Find("HeartContainer").transform.localScale = new Vector3(0, 0, 0);
            // Destroy Gravity Grenade
            
        }
        else {
            ReleaseHook();

            animator.SetTrigger("isHit");
        }
    }

    //functions called by animator at start of respective anim
    public void setIsHit() {
        _rigidbody2D.velocity = new Vector2(0, 0);
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
        gravForce = startingGravForce;
        totalJumps = 0;
        isDead = false;
    }

    IEnumerator SlowDownTime(float start, float end, float time)    
    {
        
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Debug.Log('p');
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Debug.Log("done");
        Time.timeScale = end;
    }
}

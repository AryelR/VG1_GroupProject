using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    //outlet
    Rigidbody2D _rigidbody;

    //config
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft; 
    public KeyCode keyRight;
    public float moveSpeed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //Physics.gravity = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(keyUp))
        {
            _rigidbody.AddForce(Vector2.up * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if(Input.GetKey(keyDown))
        {
            _rigidbody.AddForce(Vector2.down * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if(Input.GetKey(keyLeft))
        {
            _rigidbody.AddForce(Vector2.left * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if( Input.GetKey(keyRight))
        {
            _rigidbody.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    float maxSpeed = 2f;
    [SerializeField]
    bool bkinematic;

    //Private
    Rigidbody2D rigidBody;
    bool bFacingRight = true;
    float moveX = 0f, moveY = 0f;

    //Public
    public Transform groundCheck;
    public LayerMask player;
    public float groundColliderRadius;
    public bool bgrounded;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody.bodyType == RigidbodyType2D.Kinematic)
        {
            bkinematic = true;
        }
    }

    void FlipSprite()
    {
        bFacingRight = !bFacingRight;
        Vector3 spriteLocalScale = transform.localScale;
        spriteLocalScale.x *= -1;
        transform.localScale = spriteLocalScale;
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        bgrounded = Physics2D.OverlapCircle(groundCheck.position, groundColliderRadius, player);
        if (bkinematic)
        {
            rigidBody.velocity = new Vector2(maxSpeed * moveX, maxSpeed * moveY);
        }
        else
        {
            rigidBody.velocity = new Vector2(maxSpeed * moveX, rigidBody.velocity.y);
        }
        
        if (moveX > 0 && !bFacingRight|| moveX<0&& bFacingRight)
        {
            FlipSprite();
        }
    }
}

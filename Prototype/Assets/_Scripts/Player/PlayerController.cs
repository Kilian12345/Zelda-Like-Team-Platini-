using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveHorizontal;
    public float moveVertical;
    public float speed;
    public Rigidbody2D player;
    public Animator animator;
    private Vector2 movement;

    //Player Movement
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * speed;
        moveVertical = Input.GetAxis("Vertical") * speed;

        movement = new Vector2(moveHorizontal, moveVertical);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertivcal", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        player.velocity = movement * speed;
    }
    /*
    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }*/
}

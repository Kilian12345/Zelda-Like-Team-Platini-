using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveHorizontal;
    public float moveVertical;
    public float speed;

    public int health;
    public int selectedAbility;

    public Rigidbody2D player;
    public Animator animator;

    private Vector2 movement;

    private void Start()
    {
        health = 0;
        selectedAbility = 0;
    }

    //Player Movement
    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * speed;
        moveVertical = Input.GetAxis("Vertical") * speed;

        //magnitude = sqrt(x*x + y*y + z*z) so whenever the player is moving x,y or z > 1 so magnitude > 1
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        player.velocity = movement * speed;
    }

    private void Update()
    {
        Attack();
    }
    
    private void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack");
            animator.SetBool("Attack", true);
            Vector2 mov = new Vector2(0.0f, 1.0f);
            animator.SetFloat("Horizontal", mov.x);
            animator.SetFloat("Vertical", mov.y);
            //StartCoroutine(WaitFire());
        }
        
        /*IEnumerator WaitFire()
        {
            yield return new WaitForSeconds(0.5f);
        }*/
    }

    private void Abilities()
    {
        animator.SetBool("AbilityActive", true);
    }

}

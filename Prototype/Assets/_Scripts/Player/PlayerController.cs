using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("/// Scripted values ///")]
    public float moveHorizontal;
    public float moveVertical;
    public float horizontal;
    public float vertical;
    [Header("/// Hand values ///")]
    public float speed;
    public float speedNormal = 1;
    [Header("/// In progress ///")]
    public int health;
    public int selectedAbility;

    public Rigidbody2D player;
    public Animator animator;

    [SerializeField] float speedInCombat = 0;
    private Vector2 movement;
    private Vector2 mov;
    private Vector2 movWithoutSpeed;

    private bool AttackAllowed = true;
    private bool AbilityAllowed = true;

    private void Start()
    {
        health = 0;
        selectedAbility = 0;
    }

    private void Update()
    {
        Movement();
        StartCoroutine(Attack());
        StartCoroutine(Abilities());
    }
    
    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movWithoutSpeed = new Vector2(horizontal, vertical);

        if (horizontal > 0)
        {
            horizontal = 1;
        }
        if (horizontal < 0)
        {
            horizontal = -1;
        }
        if (vertical > 0)
        {
            vertical = 1;
        }
        if (vertical < 0)
        {
            vertical = -1;
        }

        moveHorizontal = horizontal * speed;
        moveVertical = vertical * speed;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        player.velocity = movement * speed;

        //magnitude = sqrt(x*x + y*y + z*z) so whenever the player is moving x,y or z > 1 so magnitude > 1
        animator.SetFloat("Horizontal", movWithoutSpeed.x);
        animator.SetFloat("Vertical", movWithoutSpeed.y);
        animator.SetFloat("Magnitude", movWithoutSpeed.magnitude);
    }

    #region /// ANIMATION

    IEnumerator Attack()
    {

        if (Input.GetButtonDown("Attacking") && AbilityAllowed == true && AttackAllowed == true)
        {
            AbilityAllowed = false;
            AttackAllowed = false;
            animator.SetBool("Attacking", true);

            animator.SetFloat("Horizontal", movWithoutSpeed.x);
            animator.SetFloat("Vertical", movWithoutSpeed.y);

            speed = speedInCombat;
            yield return new WaitForSeconds(1f);
            AbilityAllowed = true;
            AttackAllowed = true;
            animator.SetBool("Attacking", false);
            speed = speedNormal;
        }
    }

    IEnumerator Abilities()
    {

        if ((Input.GetButtonDown("Ability1") || Input.GetButtonDown("Ability2") || Input.GetButtonDown("Ability3")) && AttackAllowed == true && AbilityAllowed == true)
        {
            AttackAllowed = false;
            AbilityAllowed = false;
            animator.SetBool("AbilityActive", true);

            if (Input.GetButtonDown("Ability1"))
            {
                Vector2 mov = Vector2.right;
                animator.SetFloat("PosX", mov.x);
                animator.SetFloat("PosY", mov.y);
            }
            if (Input.GetButtonDown("Ability2"))
            {
                Vector2 mov = Vector2.up;
                animator.SetFloat("PosX", mov.x);
                animator.SetFloat("PosY", mov.y);
            }
            if (Input.GetButtonDown("Ability3"))
            {
                Vector2 mov = Vector2.left;
                animator.SetFloat("PosX", mov.x);
                animator.SetFloat("PosY", mov.y);
            }

            speed = speedInCombat;
            yield return new WaitForSeconds(1.2f);
            AttackAllowed = true;
            AbilityAllowed = true;
            animator.SetBool("AbilityActive", false);
            speed = speedNormal;
        }
    }
    #endregion
}
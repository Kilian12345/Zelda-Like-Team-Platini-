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
    public int selectedAbility;

    public Rigidbody2D player;
    public Animator animator;

    [SerializeField] float speedInCombat = 0;
    private Vector2 movement;
    private Vector2 mov;
    private Vector2 movWithoutSpeed;
    [HideInInspector]
    public float baseMultiplier;
    private float multiplier;
    [HideInInspector]
    public bool NewActionAllowed = true;
    public bool isPushed;

    Player plScript;


    void Start()
    {
        plScript = GetComponent<Player>();
        speedInCombat = 0.5f;
        baseMultiplier = 1f;
        multiplier = baseMultiplier;
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
        Vector3 movement = new Vector2(moveHorizontal, moveVertical);
        if (!isPushed)
        {
            
            if (NewActionAllowed)
            {
                player.velocity = movement * speed * baseMultiplier*plScript.timeScaleModifier;
               // Debug.Log("Base Mul " + baseMultiplier);
            }
            else
            {
                player.velocity = movement * speed*multiplier * plScript.timeScaleModifier;
               // Debug.Log("Mul " + multiplier);
            }
        }
        animator.SetFloat("Horizontal", movWithoutSpeed.x);
        animator.SetFloat("Vertical", movWithoutSpeed.y);
        animator.SetFloat("Magnitude", movWithoutSpeed.magnitude);
    }

    #region /// ANIMATION

    IEnumerator Attack()
    {

        if (plScript.canPunch && NewActionAllowed == true)
        {
            NewActionAllowed = false;
            animator.SetBool("Attacking", true);
            multiplier = 1f;
            animator.SetFloat("Horizontal", movWithoutSpeed.x);
            animator.SetFloat("Vertical", movWithoutSpeed.y);

            speed = speedInCombat;
            yield return new WaitForSeconds(0.5f);
            NewActionAllowed = true;
            animator.SetBool("Attacking", false);
            speed = speedNormal;
            multiplier = baseMultiplier;
        }
    }

    IEnumerator Abilities()
    {
        if (/*Input.GetButtonDown("Ability1")  &&*/ plScript.activatedAbility==1)
        {
            NewActionAllowed = false;
            animator.SetBool("AbilityActive", true);
            animator.SetBool("Ability1", true);
            //speed = speedInCombat;
            yield return new WaitForSeconds(0.6f);
            NewActionAllowed = true;
            animator.SetBool("AbilityActive", false);
            animator.SetBool("Ability1", false);
            speed = speedNormal;
            multiplier = baseMultiplier;
        }
        else if (/*Input.GetButtonDown("Ability2") && */plScript.activatedAbility==2)
        {
            NewActionAllowed = false;
            animator.SetBool("AbilityActive", true);
            animator.SetBool("Ability2", true);
            //speed = speedInCombat;
            yield return new WaitForSeconds(0.5f);
            NewActionAllowed = true;
            animator.SetBool("AbilityActive", false);
            animator.SetBool("Ability2", false);
            speed = speedNormal;
            multiplier = baseMultiplier;
        }
        else if (/*Input.GetButtonDown("Ability3") &&*/ plScript.activatedAbility==3)
        {
            NewActionAllowed = false;
            animator.SetBool("AbilityActive", true);
            animator.SetBool("Ability3", true);
            //speed = speedInCombat;
            yield return new WaitForSeconds(1.2f);
            NewActionAllowed = true;
            animator.SetBool("AbilityActive", false);
            animator.SetBool("Ability3", false);
            speed = speedNormal;
            multiplier = baseMultiplier;
        }
    }
    #endregion
}
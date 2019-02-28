using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateMachineBehaviour
{
    Player ps;
    GameObject player;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0 && Mathf.Abs(Input.GetAxisRaw("Vertical")) != 0)
        {
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") / 2 * ps.thrust, Input.GetAxisRaw("Vertical") / 2 * ps.thrust), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * ps.thrust, Input.GetAxisRaw("Vertical") * ps.thrust), ForceMode2D.Force);
        }
        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0 && Mathf.Abs(Input.GetAxisRaw("Vertical")) != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal")/2 * Mathf.Pow(ps.thrust,2), Input.GetAxisRaw("Vertical")/2 * Mathf.Pow(ps.thrust, 2));
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Mathf.Pow(ps.thrust, 2), Input.GetAxisRaw("Vertical") * Mathf.Pow(ps.thrust, 2));
        }*/
        
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

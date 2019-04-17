using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : StateMachineBehaviour
{
    public Transform playerPos;
    public float speed, stopingDistance;
    Player ps;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos= GameObject.FindGameObjectWithTag("Player").transform;
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ps.EnemiesFollowing++;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (Vector2.Distance(animator.transform.position, playerPos.position) > stopingDistance && Vector2.Distance(animator.transform.position, playerPos.position) < rangeOfMovement)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed*0.5f * Time.deltaTime);
        }*/
        if (Vector2.Distance(animator.transform.position, playerPos.position) > stopingDistance)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ps.EnemiesFollowing--;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}

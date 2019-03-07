using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateMachineBehaviour
{
    private Vector2 finalPos;
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
            finalPos = new Vector2(player.transform.position.x+((ps.dashDistance)*ps.moveHor), player.transform.position.y+((ps.dashDistance) * ps.moveVer));
        }
        else
        {
            finalPos = new Vector2(player.transform.position.x + (ps.dashDistance* ps.moveHor), player.transform.position.y + (ps.dashDistance * ps.moveVer));
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector2.Distance(player.transform.position, finalPos) >= 0)
        {
            Charge();
        }
        
    }
    void Charge()
    {
        player.transform.position = Vector2.MoveTowards(player.transform.position, finalPos, ps.thrust * Time.deltaTime);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : StateMachineBehaviour
{
    public float viewRadius = 5;
    public float viewAngle = 135;
    public LayerMask obstacleMask;
    Collider2D[] playerInRadius;
    public List<Transform> visiblePlayer = new List<Transform>();
    public bool PlayerDetected = false;
    public float DamageDeal = 1;
    GameObject pl;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pl = GameObject.FindGameObjectWithTag("Player");
        viewRadius = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindVisiblePlayer();
        ZoneIncrease();
    }
    void FindVisiblePlayer()
    {

        playerInRadius = Physics2D.OverlapCircleAll(pl.transform.position, viewRadius, LayerMask.GetMask("Ennemy"));

        visiblePlayer.Clear();

        for (int i = 0; i < playerInRadius.Length; i++)
        {
            Transform player = playerInRadius[i].transform;
            //Transform tagObjectif = GameObject.FindWithTag("Player").transform;
            Vector2 dirPlayer = new Vector2(player.position.x - pl.transform.position.x, player.position.y - pl.transform.position.y);

            Debug.DrawLine(player.transform.position, pl.transform.position, Color.red);

            if (Vector2.Angle(dirPlayer, pl.transform.right) < viewAngle / 2)
            {
                float distancePlayer = Vector2.Distance(pl.transform.position, player.position);

                if (Physics2D.Raycast(pl.transform.position, dirPlayer, distancePlayer, obstacleMask))
                {

                    visiblePlayer.Add(player);
                    PlayerDetected = true;

                }




            }

        }

    }
    void ZoneIncrease()
    {
        float puissance = (float)1.00001;
        viewRadius = Mathf.Clamp(((viewRadius * puissance) * (float)1.1), (float)0.1, 5);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        viewRadius = 0;
    }

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

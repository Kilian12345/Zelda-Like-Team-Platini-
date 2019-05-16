using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiserController : MonoBehaviour
{

    JumpAttack jumpAttack;
    DashAttack dashAttack;
    PreciseStrike preciseStrike;
    BulletHell bulletHell;

    EnemyHealth healthScript;

    
    public float phase1ExitPercent;
    public float phase2ExitPercent;
    public float attackDuration;

    private float healthPercent;
    [SerializeField]
    private float timeToAttack1;
    [SerializeField]
    private float timeToAttack2;
    [SerializeField]
    private float timeToAttack3;
    [SerializeField]
    private int curAttack;
    private float coinToss;
    //private float curTime;

    void Start()
    {
        healthScript = GetComponent<EnemyHealth>();
        jumpAttack = GetComponent<JumpAttack>();
        dashAttack = GetComponent<DashAttack>();
        preciseStrike = GetComponent<PreciseStrike>();
        bulletHell = GetComponent<BulletHell>();
    }

    void FixedUpdate()
    {
        checkForState();
    }

    void checkForState()
    {
        Debug.Log("Time " + Time.time);
        healthPercent = (healthScript.health / healthScript.maxHealth) * 100;
        if (healthPercent >= phase1ExitPercent)
        {
            Phase1();
        }
        else if (healthPercent < phase1ExitPercent && healthPercent >= phase2ExitPercent)
        {
            Phase2();
        }
        else if (healthPercent < phase2ExitPercent && healthPercent >= 0)
        {
            Phase3();
        }
    }

    void Phase1()
    {
        jumpAttack.Enabled = false;
        bulletHell.Enabled = false;
        if (Time.time > timeToAttack1)
        {
            timeToAttack1 = Time.time + attackDuration;
            if (curAttack <= 1)
            {
                switch (curAttack)
                {
                    case 0:
                        {
                            dashAttack.Enabled = true;
                            preciseStrike.Enabled = false;
                        }
                        break;
                    case 1:
                        {
                            dashAttack.Enabled = false;
                            preciseStrike.Enabled = true;
                        }
                        break;
                    default:
                        {
                            dashAttack.Enabled = true;
                            preciseStrike.Enabled = false;
                        }
                        break;
                }
                curAttack++;
            }
            else
            {
                curAttack = 0;
            }
        }
    }

    void Phase2()
    {
        dashAttack.Enabled = false;
        preciseStrike.Enabled = false;
        if (Time.time > timeToAttack2)
        {
            timeToAttack2 = Time.time + attackDuration;
            if (curAttack <= 1)
            {
                switch (curAttack)
                {
                    case 0:
                        {
                            jumpAttack.Enabled = true;
                            bulletHell.Enabled = false;
                        }
                        break;
                    case 1:
                        {
                            jumpAttack.Enabled = false;
                            bulletHell.Enabled = true;
                        }
                        break;
                    default:
                        {
                            jumpAttack.Enabled = true;
                            bulletHell.Enabled = false;
                        }
                        break;
                }
                curAttack++;
            }
            else
            {
                curAttack = 0;
            }
        }
    }

    void Phase3()
    {
        if (Time.time > timeToAttack3)
        {
            timeToAttack3 = Time.time + attackDuration;
            if (curAttack <= 1)
            {
                switch (curAttack)
                {
                    case 0:
                        {
                            preciseStrike.Enabled = false;
                            bulletHell.Enabled = false;
                            coinToss = Random.Range(0, 1);
                            switch (Mathf.RoundToInt(coinToss))
                            {
                                case 0:
                                    {
                                        dashAttack.Enabled = true;
                                        jumpAttack.Enabled = false;
                                        
                                    }
                                    break;
                                case 1:
                                    {
                                        dashAttack.Enabled = false;
                                        jumpAttack.Enabled = true;
                                    }
                                    break;
                            }
                        }
                        break;
                    case 1:
                        {
                            dashAttack.Enabled = false;
                            jumpAttack.Enabled = false;
                            coinToss = Random.Range(0, 1);
                            switch (Mathf.RoundToInt(coinToss))
                            {
                                case 0:
                                    {
                                        preciseStrike.Enabled = true;
                                        bulletHell.Enabled = false;

                                    }
                                    break;
                                case 1:
                                    {
                                        preciseStrike.Enabled = false;
                                        bulletHell.Enabled = true;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                curAttack++;
            }
            else
            {
                curAttack = 0;
            }
        }
    }

}

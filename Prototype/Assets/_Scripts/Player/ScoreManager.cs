using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public Player ps;
    private int EnemyIncrement;
    private float ScoreIncrement;



    public void TakeDamage(float dam)
    {
       ps.PlayerScore += ScoreIncrement;
    }
}

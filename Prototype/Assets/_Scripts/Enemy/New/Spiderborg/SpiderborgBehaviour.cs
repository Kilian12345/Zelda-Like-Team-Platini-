using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

has 3 behaviours based on his life to get from enemyhealth
>= 70 : rocket style ulti gibraltar
spawn x missiles gameobjects randomly in a cercle one after the other for 5sec, gets imunity state
<= 69 && >= 40 : lazerbeam
<= 39 && >= 1 : jump on player

detects player
check life state
select behaviour

private class movetoward with range variables
private class imunity
private class die ? check enemyhealth if it exists already

*/

/*public class SpiderborgBehaviour : MonoBehaviour
{
    int attackState = 0;
    bool playerDetected = false;
    public GameObject roamingPoint;

    void Update()
    {
        if (playerDetected)
        {

        }
        //player detected false state0
        //player detected true
        //if health >= 70 state1
        //if health <= 69 && >= 40 state2
        //if health <= 39 && >= 1 state3
    }

    void attack0()
    {
        //idle stance, doesn't do anything
    }
    void attack1()
    {
        //rocket barrage
    }
    void attack2()
    {
        //lazerbeam
    }
    void attack3()
    {
        //jump on player
    }
}*/
public class SpiderborgBehaviour : MonoBehaviour
{

    public GameObject StrikeZone;

    int numStrike = 9;
    int numStrike2 = 18;
    int numStrike3 = 18;
    int i, j, k;

    float[] angle1 = { -190, -150, -110, -70, -30, 10, 50, 90, 130, 170 };
    float[] angle2 = { -190, -170, -150, -130, -110, -90, -70, -50, -30, -10, 10, 30, 50, 70, 90, 110, 130, 150, 170 };
    float[] angle3 = { -190, -170, -150, -130, -110, -90, -70, -50, -30, -10, 10, 30, 50, 70, 90, 110, 130, 150, 170 };
    float cercleSize = 0.2f;
    float cercleSize2 = 0.4f;
    float cercleSize3 = 0.6f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I");
            Attack3();
        }
    }

    #region Attack3
    void Attack3()
    {
        Vector2 center = transform.position;
        for (i = 0; i < numStrike; i++)
        {
            Vector2 pos = RandomCircle(center, cercleSize);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(StrikeZone, pos, Quaternion.identity);
        }
        Vector2 center2 = transform.position;
        for (j = 0; j < numStrike2; j++)
        {
            Vector2 pos2 = RandomCircle2(center2, cercleSize2);
            Quaternion rot2 = Quaternion.FromToRotation(Vector3.forward, center2 - pos2);
            Instantiate(StrikeZone, pos2, Quaternion.identity);
        }
        Vector2 center3 = transform.position;
        for (k = 0; k < numStrike3; k++)
        {
            Vector2 pos3 = RandomCircle3(center3, cercleSize3);
            Quaternion rot3 = Quaternion.FromToRotation(Vector3.forward, center3 - pos3);
            Instantiate(StrikeZone, pos3, Quaternion.identity);
        }
    }
    
    Vector2 RandomCircle(Vector2 center, float radius)
    {
        //float ang = Random.value * 360;
        float ang = angle1[i];
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    Vector2 RandomCircle2(Vector2 center, float radius)
    {
        //float ang = Random.value * 360;
        float ang = angle2[j];
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    Vector2 RandomCircle3(Vector2 center, float radius)
    {
        //float ang = Random.value * 360;
        float ang = angle3[k];
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    #endregion
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

detects player
check life for behaviour
select behaviour

class movetoward with range variables
class imunity
class die ? check enemyhealth if it exists already

*/

/*public class SpiderborgBehaviour : MonoBehaviour
{
    int attackState = 0;
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

    [SerializeField] int numStrike1;
    [SerializeField] int numStrike2;
    [SerializeField] int numStrike3;
    int i, j, k;
    [SerializeField] [Range(-190, 190)] float rangeAngle1;
    [SerializeField] [Range(-190, 190)] float rangeAngle2;
    [SerializeField] [Range(-190, 190)] float rangeAngle3;

    [SerializeField] bool Small;
    [SerializeField] bool Medium;
    [SerializeField] bool Big;

    [SerializeField] float cercleSize;
    float cercleSize2, cercleSize3;

    private void Start()
    {
        i = numStrike1 - 1;
        j = numStrike2 - 1;
        k = numStrike3 - 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I");
            Attack3();
        }
        if (Small)
        {
            numStrike1 = 6;
            numStrike2 = 12;
            numStrike3 = 18;
            rangeAngle1 = 60;
            rangeAngle2 = 30;
            rangeAngle3 = 20;
            cercleSize = 0.2f;
        }
        else if (Medium)
        {
            numStrike1 = 9;
            numStrike2 = 19;
            numStrike3 = 24;
            rangeAngle1 = 40;
            rangeAngle2 = 20;
            rangeAngle3 = 15;
            cercleSize = 0.3f;
        }
        else if (Big)
        {
            numStrike1 = 14;
            numStrike2 = 25;
            numStrike3 = 36;
            rangeAngle1 = 30;
            rangeAngle2 = 15;
            rangeAngle3 = 10;
            cercleSize = 0.4f;
        }
        cercleSize2 = cercleSize * 2;
        cercleSize3 = cercleSize * 3;
    }

    #region Attack3
    void Attack3()
    {
        Vector2 center = transform.position;
        for (i = 0; i < numStrike1; i++)
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
        float angle1 = i * rangeAngle1;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle1 * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle1 * Mathf.Deg2Rad);
        return pos;
    }
    Vector2 RandomCircle2(Vector2 center, float radius)
    {
        float angle2 = j * rangeAngle2;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle2 * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle2 * Mathf.Deg2Rad);
        return pos;
    }
    Vector2 RandomCircle3(Vector2 center, float radius)
    {
        float angle3 = k * rangeAngle3;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle3 * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle3 * Mathf.Deg2Rad);
        return pos;
    }
    #endregion
}



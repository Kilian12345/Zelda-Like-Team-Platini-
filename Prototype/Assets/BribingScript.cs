using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BribingScript : MonoBehaviour
{
    [SerializeField] GameObject BribeInt;
    [SerializeField] Rigidbody2D Door;
    [SerializeField] Rigidbody2D BossDoor;
    [SerializeField] Player pl;
    [SerializeField] SpiderborgBehaviour SB;
    private bool payed = false;
    private float speed = 1.0f;
    private Vector2 currentpos;
    private Vector2 currentpos2;
    public Transform targetpos;
    public Transform targetpos2;
    private bool canMove;
    private bool canMove2 = false;

    private void Start()
    {
        
    }

    void Update()
    {
        if(SB.isEnded == true)
        {
            canMove2 = true;
        }
        currentpos = Door.transform.position;
        currentpos2 = BossDoor.transform.position;

        if (canMove2 == true)
        {
            BossDoor.transform.position = Vector2.MoveTowards(currentpos2, targetpos2.position, speed * Time.deltaTime);
        }
        if(Vector2.Distance(currentpos2, targetpos2.position) <= 0.05f)
        {
            canMove2 = false;
        }

        if (payed == false)
        {
            Paying();
        }
        if (canMove)
        {
            Door.transform.position = Vector2.MoveTowards(currentpos, targetpos.position, speed * Time.deltaTime);
        }
        if (Vector2.Distance(currentpos, targetpos.position) <= 0.05f)
        {
            canMove = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && payed == false)
        {
            BribeInt.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && payed == false)
        {
            BribeInt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            BribeInt.SetActive(false);
        }
    }

    void Paying()
    {
        if (Input.GetButtonDown("Bribing") && pl.PlayerScore >= 500)
        {
            payed = true;
            pl.PlayerScore -= 500;
            BribeInt.SetActive(false);
            canMove = true;
        }
    }
}

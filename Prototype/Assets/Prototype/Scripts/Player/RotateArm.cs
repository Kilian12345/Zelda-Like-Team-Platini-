using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    SpriteRenderer sr;
    public float moveHor, moveVer, lastHor, lastVer, angle;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        if (moveVer != 0 || moveHor != 0)
        {
            lastVer = moveVer;
            lastHor = moveHor;
        }
        angle = Mathf.Atan2(lastHor, lastVer) * Mathf.Rad2Deg;
        if (moveVer > 0)
        {
            sr.sortingOrder = 0;
        }
        else
        {
            sr.sortingOrder = 0;
        }
        if (moveHor > 0.2f)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else if (moveHor < -0.2f)
        {
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }
}

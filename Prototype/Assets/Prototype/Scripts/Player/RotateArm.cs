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
        if (moveVer > 0)
        {
            sr.sortingOrder = -1;
        }
        else
        {
            sr.sortingOrder = 0;
        }
        angle = Mathf.Atan2(lastHor, lastVer) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
    }
}

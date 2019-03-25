using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector2 velocity;
    public float smoothDampX = 0.05f;
    public float smoothDampY = 0.05f;
    public GameObject player;
    public float posX, posY;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void LateUpdate()
    {
        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothDampX);
        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothDampY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

}

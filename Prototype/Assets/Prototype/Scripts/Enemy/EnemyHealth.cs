using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public AudioSource enemy2Audio;
    public AudioClip dead, punch;
    public GameObject particles;
    public float health;

    [SerializeField]
    private int scorePerHit;

    PlayerMovement pm;

    void Start()
    {
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            enemy2Audio.clip = dead;
            enemy2Audio.Play();
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.5f);
        }
    }


    public void TakeDamage(float dam)
    {
        enemy2Audio.clip = punch;
        enemy2Audio.Play();
        health -= dam;
        if (health > 0)
        {
            pm.PlayerScore += ((100 - pm.health) / 100) * (scorePerHit);
            Debug.Log("Punch " + (100 - pm.health) / 100);
        }
        
    }

}

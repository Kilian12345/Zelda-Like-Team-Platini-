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

    Player ps;

    void Start()
    {
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            ps.PlayerScore += ((100 - ps.health) / 100) * (scorePerHit);
            Debug.Log("Punch " + (100 - ps.health) / 100);
        }
        
    }

}

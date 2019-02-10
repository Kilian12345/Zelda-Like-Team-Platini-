using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public AudioSource enemy2Audio;
    public AudioClip dead, punch;
    public GameObject particles;
    public float health;

    ThirdAbility ThAb;
    EnemyAI AI;

    void Start()
    {
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        ThAb = FindObjectOfType<ThirdAbility>();
        AI = FindObjectOfType<EnemyAI>();
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

        ZoneDamage();

        Debug.Log(health);
    }

    public void TakeDamage(float dam)
    {
        enemy2Audio.clip = punch;
        enemy2Audio.Play();
        health -= dam;
    }

    public void ZoneDamage()
    {
        if ( ThAb.player == gameObject.transform)
        {
            StartCoroutine(Damage());
        }
        else
        {
            AI.speed = 100;
        }
    }

    IEnumerator Damage()
    {
        AI.speed = 0;
        yield return new WaitForSeconds(1);
        health = Mathf.Clamp(health - ThAb.DamageDeal, -10, 100);      
    }

}

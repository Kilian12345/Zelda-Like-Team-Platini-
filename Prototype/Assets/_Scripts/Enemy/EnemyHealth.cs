﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    AudioSource enemyAudio;
    public AudioClip dead, punch;
    public GameObject particlesBlood;
    public GameObject particlesThanos;
    public GameObject HitpointsParentPrefab;
    public GameObject parent;
    public float health;
    public float dissolveAmout;

    [SerializeField]
    private int scorePerHit;

    bool asExploded;
    public bool getDissolve = false;
    bool zoneDeath = false;
    Player ps;
    Transform pl;
    [SerializeField]Vector3 miniBoss; 
    Vector3 whoThanos; 

    ThirdAbility ThAb;
    Fow_Parent laserZone;
    Rendering_Chara renderChara;

    void Start()
    {
        enemyAudio = gameObject.GetComponent<AudioSource>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ThAb = FindObjectOfType<ThirdAbility>();
        laserZone = FindObjectOfType<Fow_Parent>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        renderChara = GetComponent<Rendering_Chara>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0;

            if (getDissolve == false) {NormalDeath();}
            else {DissolveDeath();}
        }

        if (Vector2.Distance(transform.position, pl.position) > 0.2)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

            ZoneDamage();

            dissolveAmout = renderChara.dissolveAmout; /////// For Export

    }


    public void TakeDamage(float dam)
    {
        enemyAudio.clip = punch;
        enemyAudio.Play();
        Debug.Log("TakeDamage");
        renderChara.isOpaque = true;

        health -= dam;

        /*if (health > 0)
        {
            /*ps.PlayerScore += ((100 - ps.health) / 100) * (scorePerHit);
            
            Debug.Log("Punch " + (100 - ps.health) / 100);
        }*/

        if (HitpointsParentPrefab && health >= 0)
        {
            ShowHitpointsParent();
            ps.PlayerScore += scorePerHit;
        }
    }

    void ShowHitpointsParent()
    {
        var go = Instantiate(HitpointsParentPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponentInChildren<Text>().text = scorePerHit.ToString();
        go.GetComponent<Canvas>().sortingLayerName = "UI Effect";
        
        if (pl.position.x > transform.position.x)
        {
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        else
        {
            go.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
    }

    public void ZoneDamage()
    {
        if (ThAb.visiblePlayer.Contains(this.transform))
        {StartCoroutine(ThirdDamage());}
        else if (laserZone.visiblePlayer.Contains(this.transform))
        {StartCoroutine(LaserDamage());}

        if (health <= 0 && getDissolve == true)
        {getDissolve = true;}


    }

    void Blood()
    {
        if (asExploded == false)
        {
            Vector3 dir = (ps.centrePoint.transform.position - transform.position).normalized;
            Instantiate(particlesBlood, transform.position, Quaternion.LookRotation( dir * -1 ));
            asExploded = true;
        }
    }

    void Thanosed()
    {
        if (asExploded == false)
        {           
            Vector3 dir = (whoThanos - transform.position).normalized;
            Instantiate(particlesThanos, transform.position,  Quaternion.LookRotation( dir * -1 ));
            asExploded = true;
        } 
    }

    void NormalDeath()
    {
         enemyAudio.clip = dead;
         enemyAudio.Play();
         GetComponent<Collider2D>().enabled = false;
         Destroy(parent, 0.6f);
         Blood();
    }

    void DissolveDeath()
    {
        if(zoneDeath == true) {renderChara.DissolveEmission = new Color (1,1,0,1);}
        else {renderChara.DissolveEmission = new Color (1,0,1,1);}
        renderChara.isDissolve = true;
        Thanosed();

        if(renderChara.dissolveAmout >= 1.0f)
        {
           Destroy(parent, 0.6f);
        }
    }

    IEnumerator ThirdDamage()
    {
        whoThanos = ps.centrePoint.transform.position;
        zoneDeath = true;
        getDissolve = true;
        health = Mathf.Clamp(health - ThAb.DamageDeal, -10, 100);

        yield return health;
    }

        IEnumerator LaserDamage()
    {
        whoThanos = miniBoss;
        zoneDeath = false;
        getDissolve = true;
        health = Mathf.Clamp(health - laserZone.DamageDeal, -10, 100);

        yield return health;
    }

}






        



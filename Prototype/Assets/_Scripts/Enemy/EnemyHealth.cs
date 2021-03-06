﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] AudioSource enemyAudio;
    public AudioClip dead, punch, objectdestroy;
    public GameObject particlesBlood;
    public GameObject particlesThanos;
    public GameObject HitpointsParentPrefab;
    public GameObject parent;
    public float health;
    [HideInInspector]
    public float maxHealth;
    public float dissolveAmout;
    public float destroyTime;

    [SerializeField]
    private int scorePerHit;
    [SerializeField]
    private int curScore;

    bool asExploded;
    public bool getDissolve = false, isPowder;
    bool zoneDeath = false;
    bool scrShake = false; // Death ScrennShake
    bool tintBloodDone = false; //// tint ennemy bellow 50%
    bool tintBloodDoneTwo = false; //
    Player ps;
    Transform pl;
    [SerializeField]Vector3 miniBoss; 
    Vector3 whoThanos; 

    ThirdAbility ThAb;
    Fow_Parent laserZone;
    Rendering_Chara renderChara;
    FeedBack_Manager Fb_Mana;

    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ThAb = FindObjectOfType<ThirdAbility>();
        laserZone = FindObjectOfType<Fow_Parent>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        renderChara = GetComponent<Rendering_Chara>(); 
        Fb_Mana = GameObject.FindGameObjectWithTag("FeedBack_Manager").GetComponent<FeedBack_Manager>();     
        curScore = scorePerHit;
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            health = 0;

            if (getDissolve == false) 
            {             
                NormalDeath();
                Blood();
                if(scrShake == false)
                {Fb_Mana.ennemyDied = true; scrShake = true;}
            }
            else 
            {DissolveDeath();}
        }

        if (Vector2.Distance(transform.position, pl.position) > 0.2)
        {gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;}

        if (health <= 50 && tintBloodDone == false)
        {
            renderChara.Tint = new Color (1,0.5f,0.5f);
            tintBloodDone = true;
        }

            ZoneDamage();

            dissolveAmout = renderChara.dissolveAmout; /////// For Export

    }


    public void TakeDamage(float dam)
    {
        enemyAudio.clip = punch;
        enemyAudio.Play();
        renderChara.isOpaque = true;
        Fb_Mana.ennemyGetHit = true;

        health -= dam;

        /*if (health > 0)
        {
            /*ps.PlayerScore += ((100 - ps.health) / 100) * (scorePerHit);
            
            Debug.Log("Punch " + (100 - ps.health) / 100);
        }*/

        if (HitpointsParentPrefab && asExploded == false)
        { 
            if (ps.health >= 0 && ps.health < 25)
            {
                curScore = scorePerHit;
            }
            else if (ps.health >= 25 && ps.health < 50)
            {
                curScore = Mathf.RoundToInt(scorePerHit * 0.8f);
            }
            else if (ps.health >= 50 && ps.health < 75)
            {
                curScore = Mathf.RoundToInt(scorePerHit * 0.6f);
            }
            else if (ps.health >= 75 && ps.health <= 100)
            {
                curScore = Mathf.RoundToInt(scorePerHit * 0.5f);
            }
            ShowHitpointsParent();
            ps.PlayerScore += curScore;
        }
    }

    void ShowHitpointsParent()
    {
        var go = Instantiate(HitpointsParentPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponentInChildren<Text>().text = curScore.ToString();
        go.GetComponent<Canvas>().sortingLayerName = "UI Effect";

        if (transform.localScale.x==1)
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
        /*else if (laserZone.visiblePlayer.Contains(this.transform))
        {StartCoroutine(LaserDamage());}*/

    }

    void Blood()
    {
        if (asExploded == false)
        {
            enemyAudio.clip = dead;
            enemyAudio.Play();
            Vector3 dir = (pl.position - transform.position).normalized;
            Instantiate(particlesBlood, transform.position, Quaternion.LookRotation( dir * -1 ));
            Fb_Mana.StartCoroutine(Fb_Mana.vibrateBrève(Fb_Mana.vibrateTime, 0.5f, 0.5f));
            asExploded = true;
        }
    }

    void Thanosed()
    {
        if (asExploded == false)
        {
            enemyAudio.clip = dead;
            enemyAudio.Play();
            Vector3 dir = (whoThanos - transform.position).normalized;
            Instantiate(particlesThanos, transform.position,  Quaternion.LookRotation( dir * -1 ));
            asExploded = true;
        } 
    }

    void NormalDeath()
    {
         GetComponent<Collider2D>().enabled = false;
        if (destroyTime > 0)
        {
            Destroy(parent, destroyTime);
        }
        else
        {
            Destroy(parent, 0.6f);
        }
         
    }

    void DissolveDeath()
    {
        if(zoneDeath == true) {renderChara.DissolveEmission = new Color (1,0,0,1);}
        else {renderChara.DissolveEmission = new Color (1,0,1,1);}
        renderChara.isDissolve = true;
        Thanosed();

        if (renderChara.dissolveAmout >= 0.2f)
        {isPowder = true;}
        if(renderChara.dissolveAmout >= 1.0f)
        dissolveAmout = renderChara.dissolveAmout;
        {Destroy(parent, 1f);}
    }

    IEnumerator ThirdDamage()
    {
        whoThanos = ps.centrePoint.transform.position;
        zoneDeath = true;       
        health = Mathf.Clamp(health - ThAb.DamageDeal, -10, 100);

        if (health <= 0) {getDissolve = true;}

        yield return health;
    }

        IEnumerator LaserDamage()
    {
        whoThanos = miniBoss;
        zoneDeath = false;
        health = Mathf.Clamp(health - laserZone.DamageDeal, -10, 100);

        if (health <= 0) {getDissolve = true;}

        yield return health;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
         if (col.gameObject.tag == "Throwing_Object" && col.gameObject.GetComponent<ThrowingMechanic>().isThrowing )
         {
            enemyAudio.clip = objectdestroy;
            enemyAudio.Play();
            health -= 50;
             renderChara.isOpaque = true;
             col.gameObject.GetComponent<DropChance>().isDestroy = true;
             Instantiate(Fb_Mana.boxExpolsion , col.transform.position, Quaternion.identity );
             Destroy (col.gameObject, 0.05f);
             Fb_Mana.throwScrShake = true;
         }

    }

}






        



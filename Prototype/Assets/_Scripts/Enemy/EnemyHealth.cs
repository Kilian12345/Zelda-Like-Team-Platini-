using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    AudioSource enemyAudio;
    public AudioClip dead, punch;
    public GameObject particles;
    public GameObject HitpointsParentPrefab;
    public GameObject parent;
    public float health;

    [SerializeField]
    private int scorePerHit;

    bool asExploded;
    bool getDissolve = false;
    bool zoneDeath = false;
    Player ps;
    Transform pl;

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
            Debug.Log(getDissolve);    

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
        else 
        {getDissolve = false;}

    }

    void Blood()
    {
        if (asExploded == false)
        {
            Instantiate(particles, transform.position, Quaternion.identity);
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
        if(zoneDeath == true) {renderChara.DissolveEmission = new Color (0,1,1,1);}
        else {renderChara.DissolveEmission = new Color (1,0,1,1);}
        renderChara.isDissolve = true;

        if(renderChara.dissolveAmout >= 1.0f)
        {
           Destroy(parent, 0.6f);
        }
    }

    IEnumerator ThirdDamage()
    {
        zoneDeath = true;
        getDissolve = true;
        health = Mathf.Clamp(health - ThAb.DamageDeal, -10, 100);

        yield return health;
    }

        IEnumerator LaserDamage()
    {
        zoneDeath = false;
        getDissolve = true;
        health = Mathf.Clamp(health - laserZone.DamageDeal, -10, 100);

        yield return health;
    }

}






        



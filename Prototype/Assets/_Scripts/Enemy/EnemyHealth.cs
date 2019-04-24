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

    Player ps;
    Transform pl;

    ThirdAbility ThAb;

    void Start()
    {
        enemyAudio = gameObject.GetComponent<AudioSource>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ThAb = FindObjectOfType<ThirdAbility>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            enemyAudio.clip = dead;
            enemyAudio.Play();
            GetComponent<Collider2D>().enabled = false;
            Destroy(parent, 0.6f);

            Blood();
        }

        if (Vector2.Distance(transform.position, pl.position) > 0.2)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

            ZoneDamage();

    }


    public void TakeDamage(float dam)
    {
        enemyAudio.clip = punch;
        enemyAudio.Play();

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
        {
            StartCoroutine(Damage());

        }
        else
        {

        }
    }

    void Blood()
    {
        if (asExploded == false)
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            asExploded = true;
        }
    }

    IEnumerator Damage()
    {

        //yield return new WaitForSeconds(1);
        health = Mathf.Clamp(health - ThAb.DamageDeal, -10, 100);
        yield return health;
    }

}






            //Debug.Log("Punch " + (100 - ps.health) / 100);
        



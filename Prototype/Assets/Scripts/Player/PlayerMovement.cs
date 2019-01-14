using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public AudioClip hit, died;
    public AudioSource playerAudio;
    public int activatedAbility = 0;
    public GameObject particles, gun, shootPoint, rageSprite, countDownSprite, ability1Meter, ability2Meter, ability3Meter;
    public bool isDead, toPunch, isInRage;
    public float moveHor, moveVer, vel, sprintVelocity, maxVel, health, angle, attackRange, damage, rageDamage, rageTimer, rageVel, curTime, slowDownFactor, slowDownLast,abilityIsToCooldown;
    public float[] cooldownTime, curcooldownTime;
    public Text countDown;
    public float lastHor, lastVer;
    Rigidbody2D player;
    public CinemachineImpulseSource source;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        playerAudio = gameObject.GetComponent<AudioSource>();
        player = gameObject.GetComponent<Rigidbody2D>();
        health = 0;
        curTime = rageTimer;
    }

    void FixedUpdate()
    {
        anim.SetInteger("activatedAbility", activatedAbility);
        move();
        if (Input.GetButtonDown("Jump")) 
        {
            toPunch = true;
            Collider2D[] enemiestoDamage = Physics2D.OverlapCircleAll(shootPoint.transform.position, attackRange);
            //Debug.Log(enemiestoDamage.Length);
            if (enemiestoDamage.Length > 0)
            {
                for (int i = 0; i < enemiestoDamage.Length; i++)
                {
                    if(enemiestoDamage[i].GetComponent<EnemyHealth>()!=null)
                    enemiestoDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
        }
        if (toPunch)
        {
            StartCoroutine(Punch());
            gun.SetActive(true);
        }
        else
        {
            StopCoroutine(Punch());
            gun.SetActive(false);
        }
        if (health >= 100 && !isDead)
        {
            isInRage = true;
        }
        if (isInRage)
        {
            damage = rageDamage;
            vel = rageVel;
            rageSprite.SetActive(true);
            if (curTime > 0)
                curTime -= Time.deltaTime;
            else
                death();
        }
        checkForAbilityState();
        cooldownUI();
        ability1Meter.GetComponent<Image>().fillAmount = curcooldownTime[0] / cooldownTime[0];
        ability2Meter.GetComponent<Image>().fillAmount = curcooldownTime[1] / cooldownTime[1];

    }

    void checkForAbilityState()
    {
        if (health >= 0 && health < 25)
        {
            ability1Meter.SetActive(false);
            ability2Meter.SetActive(false);
            ability3Meter.SetActive(false);
        }
        else if (health >= 25 && health < 50)
        {
            ability1Meter.SetActive(true);
            ability2Meter.SetActive(false);
            ability3Meter.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Alpha1) && curcooldownTime[0] >= cooldownTime[0])
            {
                activatedAbility = 1;
                curcooldownTime[0] = cooldownTime[0];
            }
        }
        else if (health >= 50 && health < 75)
        {
            ability1Meter.SetActive(true);
            ability2Meter.SetActive(true);
            ability3Meter.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Alpha1) && curcooldownTime[0] >= cooldownTime[0])
            {
                activatedAbility = 1;
                curcooldownTime[0] = cooldownTime[0];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && curcooldownTime[1] >= cooldownTime[1])
            {
                activatedAbility = 2;
                curcooldownTime[1] = cooldownTime[1];
            }
        }
        else if (health >= 75 /*&& health < 100*/)
        {
            ability1Meter.SetActive(true);
            ability2Meter.SetActive(true);
            ability3Meter.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha1) && curcooldownTime[0] >= cooldownTime[0])
            {
                activatedAbility = 1;
                curcooldownTime[0] = cooldownTime[0];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && curcooldownTime[1] >= cooldownTime[1])
            {
                activatedAbility = 2;
                curcooldownTime[1] = cooldownTime[1];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                activatedAbility = 3;
                curcooldownTime[2] = cooldownTime[2];
            }
        }
    }

    void cooldownUI()
    {
        switch (activatedAbility)
        {
            case 0:
                {
                    if (curcooldownTime[0] < cooldownTime[0])
                    {
                        curcooldownTime[0] += Time.deltaTime;
                    }
                    if (curcooldownTime[1] < cooldownTime[1])
                    {
                        curcooldownTime[1] += Time.deltaTime;
                    }
                    if (curcooldownTime[2] < cooldownTime[2])
                    {
                        curcooldownTime[2] += Time.deltaTime;
                    }
                }
                break;
            case 1:
                {
                    if (curcooldownTime[0] < 0)
                    {
                        activatedAbility = 0;
                    }
                    else
                    {
                        curcooldownTime[0] -= (Time.deltaTime*abilityIsToCooldown);
                    }
                    if (curcooldownTime[1] < cooldownTime[1])
                    {
                        curcooldownTime[1] += Time.deltaTime;
                    }
                    if (curcooldownTime[2] < cooldownTime[2])
                    {
                        curcooldownTime[2] += Time.deltaTime;
                    }
                }
                break;
            case 2:
                {
                    if (curcooldownTime[1] < 0)
                    {
                        activatedAbility = 0;
                    }
                    else
                    {
                        curcooldownTime[1] -= (Time.deltaTime/slowDownFactor*abilityIsToCooldown);
                    }
                    if (curcooldownTime[0] < cooldownTime[0])
                    {
                        curcooldownTime[0] += Time.deltaTime;
                    }
                    if (curcooldownTime[2] < cooldownTime[2])
                    {
                        curcooldownTime[2] += Time.deltaTime;
                    }
                }
                break;
            case 3:
                {
                    if (curcooldownTime[1] < 0)
                    {
                        activatedAbility = 0;
                    }
                    else
                    {
                        curcooldownTime[1] -= (Time.deltaTime*abilityIsToCooldown);
                    }
                    if (curcooldownTime[0] < cooldownTime[0])
                    {
                        curcooldownTime[0] += Time.deltaTime;
                    }
                    if (curcooldownTime[1] < cooldownTime[1])
                    {
                        curcooldownTime[1] += Time.deltaTime;
                    }
                }
                break;
        }
    }



    void death()
    {
        isDead = true;
        playerAudio.clip = died;
        playerAudio.Play();
        Instantiate(particles, transform.position, Quaternion.identity);
        rageSprite.SetActive(true);
        vel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void move()
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVer = Input.GetAxisRaw("Vertical");
        if (moveVer != 0 || moveHor != 0)
        {
            lastVer = Mathf.Round(moveVer);
            lastHor = Mathf.Round(moveHor);
        }
        angle = Mathf.Atan2(lastHor, lastVer) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
        transform.position = new Vector2(transform.position.x + (moveHor * (vel) * Time.deltaTime), transform.position.y + (moveVer * (vel) * Time.deltaTime));

        // These if statements prevent velocity to exceed a given value

        if (player.velocity.x > maxVel)
        {
            player.velocity = new Vector2(maxVel, player.velocity.y);
        }
        if (player.velocity.x < (-maxVel))
        {
            player.velocity = new Vector2((maxVel * -1), player.velocity.y);
        }
        if (player.velocity.y > maxVel)
        {
            player.velocity = new Vector2(player.velocity.x, maxVel);
        }
        if (player.velocity.y < (-maxVel))
        {
            player.velocity = new Vector2(player.velocity.x, (maxVel * -1));
        }
        if (moveHor == 0 && moveVer == 0) // Incase input is not recieved, player stops immidiately i.e. no momentum
        {
            player.velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject, 0f);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            source.GenerateImpulse();
            playerAudio.clip = hit;
            playerAudio.Play();
            if (health < 100)
            {
                health += 10;
            }
            Destroy(col.gameObject, 0f);
        }
    }
    IEnumerator Punch()
    {
        yield return new WaitForSeconds(0.25f);
        toPunch = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.transform.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    FeedbacksOrder Fb_Order;
    Animator anim;
    AudioSource playerAudio;
    CinemachineImpulseSource source;

    public AudioClip hit, died;
    public float PlayerScore, CalciumAmount, CalciumCapacity, curDropChanceRate, DropChanceRate,EnemiesFollowing;
    public float vel, thrust,dashDistance;
    public float[] cooldownTime, curcooldownTime;
    public GameObject particles, gun, gunSprite, shootPoint, rageSprite, countDownSprite, selector;
    public GameObject[] abilityMeters;

    [HideInInspector]
    public float moveHor, moveVer;

    public Text countDown;
    public int activatedAbility = 0;
    public bool isDead, toPunch, isInRage;
    public float health, maxVel, angle, attackRange, damage, rageDamage, rageTimer, rageVel, curTime, slowDownFactor, slowDownLast, abilityIsToCooldown;
    public float lastHor, lastVer;
    Rigidbody2D player;
    private float LocalX;
    private Vector2[] abPos;
    private int selectedAbility;

    CalciumBones cb;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        playerAudio = gameObject.GetComponent<AudioSource>();
        player = gameObject.GetComponent<Rigidbody2D>();
        source = gameObject.GetComponent<CinemachineImpulseSource>();
        Fb_Order = FindObjectOfType<FeedbacksOrder>();
        LocalX = transform.localScale.x;
        health = 0;
        curTime = rageTimer;
        curDropChanceRate = DropChanceRate;
        selectedAbility = 0;
    }

    void FixedUpdate()
    {
        anim.SetInteger("activatedAbility", activatedAbility);
        anim.SetFloat("HorAxis", Mathf.Abs(moveHor));
        anim.SetFloat("VerAxis", moveVer);
        anim.SetBool("death", isDead);
        move();
        if (Input.GetButtonDown("Jump"))
        {
            toPunch = true;
        }
        if (toPunch)
        {
            StartCoroutine(Punch());
            if (lastHor >= 0f)
            {
                Vector3 to = new Vector3(0, 0, -1500);
                if (Vector3.Distance(gunSprite.transform.eulerAngles, to) > 0.01f)
                {
                    gunSprite.transform.eulerAngles = Vector3.Lerp(gunSprite.transform.rotation.eulerAngles, to, Time.deltaTime);
                }
            }
            else if (lastHor < 0f)
            {
                Vector3 to = new Vector3(0, 0, 1500);
                if (Vector3.Distance(gunSprite.transform.eulerAngles, to) > 0.01f)
                {
                    gunSprite.transform.eulerAngles = Vector3.Lerp(gunSprite.transform.rotation.eulerAngles, to, Time.deltaTime);
                }
            }

            //gunSprite.transform.eulerAngles = new Vector3(0, 0, 0);
            //gunSprite.SetActive(true);
        }
        else
        {
            StopCoroutine(Punch());
            //gunSprite.SetActive(false);
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
            if (curTime > 0 && health >= 100)
            {
                curTime -= Time.deltaTime;
            }
            else if (curTime > 0 && health < 100)
            {
                curTime = rageTimer;
                isInRage = false;
                rageSprite.SetActive(false);
            }
            else if (curTime < 0 && health >= 100)
            {
                death();
            }
        }
        if (Input.GetButton("Refill"))
        {
            StartCoroutine(refill());
        }
        abilityMeters[0].GetComponent<Image>().fillAmount = curcooldownTime[0] / cooldownTime[0];
        abilityMeters[1].GetComponent<Image>().fillAmount = curcooldownTime[1] / cooldownTime[1];
        abilityMeters[2].GetComponent<Image>().fillAmount = curcooldownTime[2] / cooldownTime[2];
        if (health > 100)
        {
            health = 100;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad6) || Input.GetButtonDown("SwitchLeft") || Input.GetButtonDown("SwitchRight"))
        {
            switchAbilities();
        }
        selectAbility();
        checkForAbilityState();
        cooldownUI();
    }

    IEnumerator refill()
    {
        if (CalciumAmount > 0 && health > 0)
        {
            CalciumAmount--;
            health--;
        }
        yield return new WaitForSeconds(0.0001f);
    }

    void switchAbilities()
    {
        if (selectedAbility >= 0 && selectedAbility <= (abilityMeters.Length - 1))
        {
            if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetButtonDown("SwitchLeft")) && selectedAbility > 0)
            {
                selectedAbility--;
            }
            if ((Input.GetKeyDown(KeyCode.Keypad6) || Input.GetButtonDown("SwitchRight")) && selectedAbility < (abilityMeters.Length - 1))
            {
                selectedAbility++;
            }
        }
        selector.transform.position = abilityMeters[selectedAbility].GetComponent<RectTransform>().position;
    }

    void selectAbility()
    {
        if (Input.GetButtonDown("AbilitySelect"))
        {
            if (abilityMeters[selectedAbility].activeSelf)
            {
                if (curcooldownTime[selectedAbility] >= cooldownTime[selectedAbility])
                {
                    activatedAbility = (selectedAbility + 1);
                    curcooldownTime[selectedAbility] = cooldownTime[selectedAbility];
                }
            }
        }
    }

    void checkForAbilityState()
    {
        if (health >= 0 && health < 25)
        {
            abilityMeters[0].SetActive(false);
            abilityMeters[1].SetActive(false);
            abilityMeters[2].SetActive(false);
        }
        else if (health >= 25 && health < 50)
        {
            abilityMeters[0].SetActive(true);
            abilityMeters[1].SetActive(false);
            abilityMeters[2].SetActive(false);
        }
        else if (health >= 50 && health < 75)
        {

            abilityMeters[0].SetActive(true);
            abilityMeters[1].SetActive(true);
            abilityMeters[2].SetActive(false);

        }
        else if (health >= 75 && health <= 100)
        {

            abilityMeters[0].SetActive(true);
            abilityMeters[1].SetActive(true);
            abilityMeters[2].SetActive(true);
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
                        curcooldownTime[0] -= (Time.deltaTime * abilityIsToCooldown);
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
                        curcooldownTime[1] -= (Time.deltaTime / slowDownFactor * abilityIsToCooldown);
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
                    if (curcooldownTime[2] < 0)
                    {
                        activatedAbility = 0;
                    }
                    else
                    {
                        curcooldownTime[2] -= (Time.deltaTime * abilityIsToCooldown);
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
        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        if (moveVer != 0 || moveHor != 0)
        {
            lastVer = moveVer;
            lastHor = moveHor;
        }
        angle = Mathf.Atan2(lastVer, lastHor) * Mathf.Rad2Deg;
        if (lastHor >= 0f)
        {
            transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
            gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (lastHor < 0f)
        {
            transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
            gun.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        }
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
            //Destroy(col.gameObject, 0f);
        }
        if (col.gameObject.tag == "Calcium")
        {
            cb = col.gameObject.GetComponent<CalciumBones>();
            for (int i = 0; i < cb.CalciumRefill; i++)
            {
                if (CalciumAmount < CalciumCapacity)
                    CalciumAmount++;
                else
                    break;
            }
            Destroy(col.gameObject, 0f);
        }
    }
    IEnumerator Punch()
    {
        Collider2D[] enemiestoDamage = Physics2D.OverlapCircleAll(shootPoint.transform.position, attackRange);
        //Debug.Log(enemiestoDamage.Length);
        if (enemiestoDamage.Length > 0)
        {
            for (int i = 0; i < enemiestoDamage.Length; i++)
            {
                if (enemiestoDamage[i].GetComponent<EnemyHealth>() != null)
                {
                    enemiestoDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    //enemiestoDamage[i].GetComponent<Rigidbody2D>.Add
                    toPunch = false;
                    break;
                }
            }
            toPunch = false;
        }
        yield return new WaitForSeconds(0.2f);
        gunSprite.transform.eulerAngles = new Vector3(0, 0, 0);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.transform.position, attackRange);
    }
}

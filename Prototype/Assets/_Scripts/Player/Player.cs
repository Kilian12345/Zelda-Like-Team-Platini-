using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// /////////////////////////////LES///GD///SONT///STYLÉS
    /// </summary>
    #region Rage

    [Header("Rage(Health) Properties ///////////////////////////////////")]

    [Header("Rage Parameters")]
    public float health;
    public float CalciumAmount;
    public float CalciumCapacity;
    public float curDropChanceRate;
    public float DropChanceRate;

    [Header("Max Rage Parameters")]
    public float rageDamage;
    public float rageTimer;
    public float rageVel;
    public float curTime;

    [Header("Rage Booleans")]
    public bool isDead;
    public bool isInRage;
    CalciumBones cb;

    #endregion


    #region Movement

    

    [Header("Movement Properties ///////////////////////////////////")]

    [Header("Movement Values")]
    [HideInInspector]
    public float moveHor, moveVer;
    public float lastHor, lastVer;
    public float vel;
    public float maxVel;
    private float LocalX;
    private Vector2 moveToPos;
    private Vector2[] abPos;
    private Vector2 dir;
    private bool canCharge;
    Rigidbody2D player;
    PlayerController plControl;

    #endregion


    #region Combat

    [Header("Combat Properties ///////////////////////////////////")]

    [Header("Combat GameObjects")]
    public GameObject gun; 
    public GameObject gunSprite;
    public GameObject shootPoint;
    public GameObject carryPoint;
    public GameObject centrePoint;

    [Header("Combat Variables")]
    public float PlayerScore;
    public float EnemiesFollowing;
    public float enemyFollowLimit;
    public float enemyBulletDamage;
    public bool toPunch;
    public float damage;
    public float baseDamage;
    public float attackSpeed;
    public float attackRange;
    public float attackPushForce;
    private float timeToAttack;
    private float angle;
    
    public bool Carry = false;
    [HideInInspector]
    public bool inStrike;
    [HideInInspector]
    public bool canPunch;

    #endregion


    #region Ability

    [Header("Ability Properties ///////////////////////////////////")]

    [Header("Ability Parameters")]
    public float[] cooldownTime;
    public float[] curcooldownTime;
    public float abilityIsToCooldown;
    [HideInInspector]
    public float timeScaleModifier;
    public int activatedAbility = 0;
    private int selectedAbility;
    public bool thirdActivated;
    
    FeedBack_Manager Fb_mana;

    [Header("Ability GameObjects")]
    public GameObject selector;
    public GameObject[] abilityMeters;

    [Header("Dash Values")]
    public float dashDistance;
    public float thrust;
    Ghost ghost;

    [Header("Slow Down Values")]
    public float slowDownFactor;
    public float slowDownLast;

    #endregion


    #region Audio Visuals

    [Header("Audio and Visual Properties ///////////////////////////////////")]

    [Header("Audio")]
    public AudioClip hit, died, punch, calcium, dash, slowmo, atk3, walk;
    [HideInInspector] public AudioSource playerAudio;

    [Header("Visuals")]
    Animator anim;
    public GameObject particles;
    CinemachineImpulseSource source;

    #endregion

    #region GUI LAYOUT

    [HideInInspector]
    public int toolbarTab;
    public string currentTab;
    #endregion

    #region Feedbacks
    public bool takeDamage = false;
    #endregion

    #region Level

    public bool levelEnd=false;
    public int sceneIndex;
    public Animator fadeAnim;
    public Image fadeImage;

    public Animator elevatorMouvEntrance;
    public GameObject elevatorEntrance;
    public Transform originParent;
    public bool iseElevatorEntrance;
    bool entranceOnce;

    public Animator elevatorMouv;
    public GameObject elevator;
    public bool usingElevator;
    public bool usingElevatorTwo;
    bool doneOneFade;
    bool fadeOut;

    #endregion

    // Use this for initialization
    void Awake()
    {
        var foundObjects = FindObjectsOfType<CalciumBones>();
        for (int i = 0; i < foundObjects.Length; i++)
        {
            Destroy(foundObjects[i]);
            Debug.Log("Destroyed");
        }

    }


    void Start()
    {
        ghost = GetComponent<Ghost>();
        anim = gameObject.GetComponent<Animator>();
        playerAudio = gameObject.GetComponent<AudioSource>();
        player = gameObject.GetComponent<Rigidbody2D>();
        source = gameObject.GetComponent<CinemachineImpulseSource>();
        plControl = gameObject.GetComponent<PlayerController>();
        Fb_mana = FindObjectOfType<FeedBack_Manager>();
        LocalX = transform.localScale.x;
        health = 0;
        curTime = rageTimer;
        curDropChanceRate = DropChanceRate;
        selectedAbility = 0;
        baseDamage = damage;
        fadeOut = true;

        if (fadeOut == true)
        { fadeAnim.SetBool("FadeOut", true); }

        timeScaleModifier = 1;

    }

    private void Update()
    {
        if (Input.GetButtonDown("Attacking") && Carry == false)
        {
            toPunch = true;
        }

        if (Input.GetButton("Refill"))
        {
            StartCoroutine(refill());
            playerAudio.clip = calcium;
            playerAudio.Play();
        }

        selectAbility();
    }

    void FixedUpdate()
    {
        if (iseElevatorEntrance == true && entranceOnce == false) /// entrance
        {
            StartCoroutine(FadingNewScene());
            entranceOnce = true;
        }

        anim.SetInteger("activatedAbility", activatedAbility);
        anim.SetFloat("lastHor", lastHor);
        anim.SetFloat("lastVer", lastVer);
        //anim.SetFloat("HorAxis", Mathf.Abs(moveHor));
        //anim.SetFloat("VerAxis", moveVer);
        anim.SetBool("Dead", isDead);
        move();
        /*if (Input.GetButtonDown("Attacking") && Carry == false) /////////////////////////// Void Update
        {
            toPunch = true;
            playerAudio.clip = punch;
            playerAudio.Play();
        }*/

        if (Time.time > timeToAttack)
        {
            //player.velocity = Vector3.zero;
            if (toPunch)
            {
                canPunch = true;
                timeToAttack = Time.time + 1 / (attackSpeed * timeScaleModifier);
                StartCoroutine(Punch());
                if (lastHor >= 0f)
                {
                    Vector3 to = new Vector3(0, 0, -2500);
                    if (Vector3.Distance(gunSprite.transform.eulerAngles, to) > 0.01f)
                    {
                        gunSprite.transform.eulerAngles = Vector3.Lerp(gunSprite.transform.rotation.eulerAngles, to, Time.deltaTime);
                    }
                }
                else if (lastHor < 0f)
                {
                    Vector3 to = new Vector3(0, 0, 2500);
                    if (Vector3.Distance(gunSprite.transform.eulerAngles, to) > 0.01f)
                    {
                        gunSprite.transform.eulerAngles = Vector3.Lerp(gunSprite.transform.rotation.eulerAngles, to, Time.deltaTime);
                    }
                }
            }

        }


        if (health >= 100 && !isDead)
        {
            isInRage = true;
        }
        if (isInRage)
        {
            damage = rageDamage;
            vel = rageVel;
            if (curTime > 0 && health >= 100)
            {
                curTime -= Time.deltaTime;
            }
            else if (curTime > 0 && health < 100)
            {
                curTime = rageTimer;
                isInRage = false;
            }
            else if ((curTime < 0 && health >= 100) && isDead == false)
            {
                death();
            }
        }

        if (levelEnd && doneOneFade == false)
        {
            StartCoroutine(FadingNextScene());
            doneOneFade = true;
        }

        checkForAbilityState();
        cooldownUI();
        updateEnemyFollowing();
    }

    void updateEnemyFollowing()
    {
        if (EnemiesFollowing <= 0)
            EnemiesFollowing = 0;
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

    #region /// ABILITIES

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

    void selectAbility() /////////////////////////////////// Nigga what's that
    {

        if (Input.GetButtonDown("Ability1") && Carry == false)
        {
            if (abilityMeters[0].activeSelf)
            {
                playerAudio.clip = dash;
                playerAudio.Play();

                if (curcooldownTime[0] >= cooldownTime[0])
                {
                    StartCoroutine(firstAbilityWait());
                }

            }
        }
        if (Input.GetButtonDown("Ability2") && Carry == false)
        {
            if (abilityMeters[1].activeSelf)
            {
                playerAudio.clip = slowmo;
                playerAudio.Play();

                if (curcooldownTime[1] >= cooldownTime[1])
                {activatedAbility = 2;}
            }

        }
        if (Input.GetButtonDown("Ability3") && Carry == false)
        {
            if (abilityMeters[2].activeSelf)
            {
                playerAudio.clip = atk3;
                playerAudio.Play();

                if (curcooldownTime[2] >= cooldownTime[2])
                {activatedAbility = 3;}
            }
        }
    }

    void secondAbility()
    {
       curcooldownTime[1] = cooldownTime[1];
       Fb_mana.secondActivated = true;
       Fb_mana.timeSecondAbility = cooldownTime[0];
    }

    void thirdAbility()
    {
        curcooldownTime[2] = cooldownTime[2];
        thirdActivated = true;

        Fb_mana.StartCoroutine(Fb_mana.vibrateBrève(1.5f, 0.5f, 0.5f)); //// what f*** timing
        
        if (activatedAbility == 0)
        {
           thirdActivated = false;
        }

    }

    void checkForAbilityState()
    {
        if (health >= 0 && health < 25)
        {
            damage = baseDamage;
            plControl.baseMultiplier = 1f;
            abilityMeters[0].SetActive(false);
            abilityMeters[1].SetActive(false);
            abilityMeters[2].SetActive(false);
        }
        else if (health >= 25 && health < 50)
        {
            damage = baseDamage * 0.8f;
            plControl.baseMultiplier = 1.04f;
            abilityMeters[0].SetActive(true);
            abilityMeters[1].SetActive(false);
            abilityMeters[2].SetActive(false);
        }
        else if (health >= 50 && health < 75)
        {
            damage = baseDamage * 0.6f;
            plControl.baseMultiplier = 1.08f;
            abilityMeters[0].SetActive(true);
            abilityMeters[1].SetActive(true);
            abilityMeters[2].SetActive(false);

        }
        else if (health >= 75 && health <= 100)
        {
            damage = baseDamage * 0.5f;
            plControl.baseMultiplier = 1.12f;
            abilityMeters[0].SetActive(true);
            abilityMeters[1].SetActive(true);
            abilityMeters[2].SetActive(true);
        }
    }

    void cooldownUI() ///////////////////////// Nigga 
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

    #endregion


    void death()
    {
        isDead = true;
        anim.SetBool("Dead", true);
        playerAudio.clip = died;
        playerAudio.Play();
        Instantiate(particles, transform.position, Quaternion.identity);
        vel = 0;
        Invoke("restart", 0.7f);
        //Destroy(gameObject,0.5f);
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadingNewScene()///////////////////////////////////////////////////////
    {

        elevatorMouvEntrance.SetBool("Entrance", true);
        transform.parent = elevatorEntrance.transform;

        yield return new WaitForSeconds(4f);

        transform.parent = originParent;
    }

    IEnumerator FadingNextScene()///////////////////////////////////////////////////
    {
        fadeAnim.SetBool("Fade", true);

        if (usingElevator == true)
        {
            elevatorMouv.SetBool("Mouv", true);
            transform.parent = elevator.transform;
        }
        else if (usingElevatorTwo == true)
        {
            elevatorMouv.SetBool("Mouv2", true);
            transform.parent = elevator.transform;
        }

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(sceneIndex);
    }

    void OnApplicationQuit()
    {
        var foundObjects = FindObjectsOfType<CalciumBones>();
        for (int i = 0; i < foundObjects.Length; i++)
        {
            Destroy(foundObjects[i]);
            Debug.Log("Quit Destroyed");
        }
    }

    void OnDisable()
    {
        var foundObjects = FindObjectsOfType<CalciumBones>();
        for (int i = 0; i < foundObjects.Length; i++)
        {
            Destroy(foundObjects[i]);
            Debug.Log("Disable Destroyed ");
        }
    }


    void move()
    {

        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        dir = new Vector2(moveHor, moveVer);
        if (moveVer != 0 || moveHor != 0)
        {
            lastVer = moveVer;
            lastHor = moveHor;
            /*playerAudio.clip = walk;
            playerAudio.Play();*/
        }
        gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        angle = Mathf.Atan2(lastVer, lastHor) * Mathf.Rad2Deg;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "LevelEnd")
        {
            if (!levelEnd)
            {
                levelEnd = true;
            }
        }
        if (col.gameObject.tag == "Strike")
        {
            inStrike = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "LevelEnd")
        {
            if (!levelEnd)
            {
                levelEnd = true;
            }
        }
        if (col.gameObject.tag == "Strike")
        {
            inStrike = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Strike")
        {
            inStrike = false;
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
                TakeDamage(enemyBulletDamage);
            }

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
        if (enemiestoDamage.Length > 0)
        {
            for (int i = 0; i < enemiestoDamage.Length; i++)
            {
                if (enemiestoDamage[i].GetComponent<EnemyHealth>() != null)
                {
                    enemiestoDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    if (enemiestoDamage[i].GetComponent<Kami>()==null)
                    {
                        enemiestoDamage[i].GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(lastHor, lastVer) * attackPushForce, ForceMode2D.Impulse);
                        yield return new WaitForSeconds(0.1f);
                        if (enemiestoDamage[i]!=null)
                        enemiestoDamage[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                    }
                    toPunch = false;
                    break;
                }

                if (enemiestoDamage[i].GetComponent<ThrowingMechanic>() != null)///////////// in Progress
                {
                    enemiestoDamage[i].GetComponent<DropChance>().isDestroy = true;
                    enemiestoDamage[i].GetComponent<ThrowingMechanic>().Destroy();
                    toPunch = false;
                    break;
                }
                else if (enemiestoDamage[i].GetComponent<Door_Break>() != null)///////////// in Progress
                {
                    enemiestoDamage[i].GetComponent<Door_Break>().Destroy();
                    toPunch = false;
                    break;
                }
            }
            toPunch = false;
            Fb_mana.ennemyGetHit = false;
        }
        yield return new WaitForSeconds(0.2f);
        toPunch = false;
        canPunch = false;
        StopCoroutine(Punch());
        gunSprite.transform.eulerAngles = new Vector3(0, 0, 0);

    }

    public void TakeDamage(float dam)
    {
        Fb_mana.StartCoroutine(Fb_mana.vibrateBrève(0.15f, 0, 0.35f));
        playerAudio.clip = hit;
        playerAudio.Play();
        health += dam;
        takeDamage = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.transform.position, attackRange);
    }

    IEnumerator firstAbilityWait()
    {
        yield return new WaitForSeconds(0.25f);
        activatedAbility = 1;
        curcooldownTime[0] = cooldownTime[0];
        Fb_mana.timeFirstAbility = cooldownTime[0];
        Fb_mana.firstActivated = true;
    }
}
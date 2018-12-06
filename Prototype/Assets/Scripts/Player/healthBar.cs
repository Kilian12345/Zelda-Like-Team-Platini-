using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    PlayerMovement pm;
    Image HealthBar;
    float maxHealth = 100;
    // Use this for initialization
    void Start()
    {
        HealthBar = GetComponent<Image>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = pm.health / maxHealth;
    }
}

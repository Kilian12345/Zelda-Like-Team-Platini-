using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    Player ps;
    Image HealthBar;
    float maxHealth = 100;
    // Use this for initialization
    void Start()
    {
        HealthBar = GetComponent<Image>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = ps.health / maxHealth;
    }
}

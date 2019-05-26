using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public GameObject boss;
    public Image slider;
    EnemyHealth healthScript;
    // Start is called before the first frame update

    void Start()
    {
        healthScript = boss.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.fillAmount= healthScript.health / healthScript.maxHealth;
    }
}

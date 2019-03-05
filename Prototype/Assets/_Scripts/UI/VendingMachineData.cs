using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachineData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] buyButtons;
    public Text[] cost,damage,range;
    public float[] costValue, damageValue, rangeValue;
    public Sprite[] weapons;

    Player ps;
    public SpriteRenderer gunSprite;

    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < cost.Length; i++)
        {
            cost[i].text = (Mathf.RoundToInt(costValue[i])).ToString();
            damage[i].text = (Mathf.RoundToInt(damageValue[i])).ToString();
            range[i].text = (rangeValue[i]).ToString();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (ps.PlayerScore > costValue[i])
            {
                buyButtons[i].SetActive(true);
            }
            else
            {
                buyButtons[i].SetActive(false);
            }
        }
    }
    /*
    public void Weapon1()
    {
        ps.PlayerScore -= costValue[0];
        gunSprite.sprite = weapons[0];
        ps.damage = damageValue[0];
        ps.attackRange = rangeValue[0];
        buyButtons[0].SetActive(false);
    }
    public void Weapon2()
    {
        ps.PlayerScore -= costValue[1];
        gunSprite.sprite = weapons[1];
        ps.damage = damageValue[1];
        ps.attackRange = rangeValue[1];
        buyButtons[1].SetActive(false);
    }
    public void Weapon3()
    {
        ps.PlayerScore -= costValue[2];
        gunSprite.sprite = weapons[2];
        ps.damage = damageValue[2];
        ps.attackRange = rangeValue[2];
        buyButtons[2].SetActive(false);
    }*/
    public void Weapon(int i)
    {
        ps.PlayerScore -= costValue[i-1];
        gunSprite.sprite = weapons[i-1];
        ps.damage = damageValue[i-1];
        ps.attackRange = rangeValue[i-1];
        buyButtons[i-1].SetActive(false);
    }

}

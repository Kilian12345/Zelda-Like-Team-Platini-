using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalciumCtr : MonoBehaviour
{
    // Start is called before the first frame update
    Player ps;
    Image CalciumBar;
    // Use this for initialization
    void Start()
    {
        CalciumBar = GetComponent<Image>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CalciumBar.fillAmount = ps.CalciumAmount / ps.CalciumCapacity;
    }
}

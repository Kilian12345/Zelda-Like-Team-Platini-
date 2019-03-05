using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject vendingMachinePanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "VendingMachine")
        {
            vendingMachinePanel.SetActive(true);
        }
        else
        {
            vendingMachinePanel.SetActive(false);
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "VendingMachine")
        {
            vendingMachinePanel.SetActive(true);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "VendingMachine")
        {
            vendingMachinePanel.SetActive(false);
        }
    }
}

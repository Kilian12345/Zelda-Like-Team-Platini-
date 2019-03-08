using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachineAssets : MonoBehaviour
{

    [SerializeField]
    GameObject canvasIntVending;

    public GameObject vendingMachinePanel;
    [SerializeField]
    private bool machineOpen, isInTrigger, timeStop;

    void FixedUpdate()
    {
        if (isInTrigger)
        {
            if ((Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.JoystickButton2)))
            {
                machineOpen = !machineOpen;
                vendingMachinePanel.SetActive(machineOpen);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canvasIntVending.SetActive(true);
            isInTrigger = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canvasIntVending.SetActive(true);
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canvasIntVending.SetActive(false);
            vendingMachinePanel.SetActive(false);
            isInTrigger = false;
        }
    }
}

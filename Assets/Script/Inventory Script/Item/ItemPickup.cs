using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    public Items Item;
    private bool isPlayerInRange = false;
    public GameObject EPopUp;

    private void Start()
    {
        EPopUp.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            EPopUp.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            EPopUp.SetActive(false);
        }
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            Pickup();
        }
    }

}

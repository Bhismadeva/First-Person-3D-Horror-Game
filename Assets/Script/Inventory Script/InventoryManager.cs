using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject inventoryPanel;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public List<Items> Items = new List<Items>();
    public List<Transform> itemSlots = new List<Transform>(); // List of item slots

    public void Start()
    {
        inventoryPanel.SetActive(false);

        // Automatically find all ItemSlot objects based on tag and add them to the list
        foreach (Transform child in ItemContent)
        {
            if (child.CompareTag("ItemSlot"))
            {
                itemSlots.Add(child);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            Instance.ListItems();
        }
    }

    public void Awake()
    {
        Instance = this;
    }

    public void Add(Items item)
    {
        Items.Add(item);
    }

    public void Remove(Items item)
    {

        Items.Remove(item);
    }

    public void ListItems()
    {
        // Clean only the instantiated items within the ItemSlot objects
        foreach (Transform slot in itemSlots)
        {
            if (slot.childCount > 0)
            {
                foreach (Transform item in slot)
                {
                    Destroy(item.gameObject);
                }
            }
        }

        // Place items in slots
        int slotIndex = 0;
        foreach (var item in Items)
        {
            if (slotIndex < itemSlots.Count)
            {
                GameObject obj = Instantiate(InventoryItem, itemSlots[slotIndex]);

                /*obj.GetComponent<ItemValue>().itemId = item.id;*/
                obj.GetComponent<UnityEngine.UI.Image>().sprite = item.icon;

                slotIndex++;
            }
        }
    }

}

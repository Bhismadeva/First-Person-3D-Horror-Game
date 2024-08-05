using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

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

    public void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            Instance.ListItems();
        }
    }

    public Items GetItems(int Id)
    {
        foreach (Items item in Items)
        {
            if (item.id == Id) return item;

        }
        return null;
    }

    public void Add(Items item)
    {
        bool itemExists = false;

        foreach (Items existingItem in Items)
        {
            if (existingItem.id == item.id)
            {
                itemExists = true;
                break; // Keluar dari loop setelah menemukan item dengan ID yang sama
            }
        }

        // Jika item tidak ada dalam daftar, tambahkan item baru
        if (!itemExists)
        {
            Items.Add(item);
        }
    }

    public void Remove(Items item)
    {
        bool itemExists = false;
        Items itemToRemove = null;

        foreach (Items existingItem in Items)
        {
            if (existingItem.id == item.id)
            {
                itemExists = true;
                itemToRemove = existingItem;
                break; // Keluar dari loop setelah menemukan item dengan ID yang sama
            }
        }

        // Jika item ada dalam daftar, hapus item tersebut
        if (itemExists)
        {
            Items.Remove(itemToRemove);
        }
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

                obj.GetComponent<ItemValue>().itemId = item.id;
                obj.GetComponent<UnityEngine.UI.Image>().sprite = item.icon;

                slotIndex++;
            }
        }
    }

}

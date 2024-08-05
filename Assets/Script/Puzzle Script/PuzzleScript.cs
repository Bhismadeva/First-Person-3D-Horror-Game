using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PuzzleScript : MonoBehaviour
{
    public static PuzzleScript Instance;

    public Transform puzzleContent;
    public List<Transform> itemSlots = new List<Transform>();
    public List<Items> items = new List<Items>();

    public GameObject Door;
    public Material newDoorMaterial;

    public int correctPlace;
    private bool[] slotChecked;
    private bool isPuzzleSolved = false;

    public void Start()
    {
        foreach (Transform child in puzzleContent)
        {
            if (child.CompareTag("ItemSlot"))
            {
                itemSlots.Add(child);
            }
        }

        slotChecked = new bool[itemSlots.Count];
    }

    public void Awake()
    {
        Instance = this;
    }

    public Items GetItems(int Id)
    {
        foreach (Items item in items)
        {
            if (item.id == Id) return item;

        }
        return null;
    }

    public void Add(Items item)
    {
        bool itemExists = false;

        foreach (Items existingItem in items)
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
            items.Add(item);
        }
    }

    public void Remove(Items item)
    {
        bool itemExists = false;
        Items itemToRemove = null;

        foreach (Items existingItem in items)
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
            items.Remove(itemToRemove);
        }
    }

    public void Update()
    {
        if (!isPuzzleSolved)
        {
            foreach (Transform slot in itemSlots)
            {
                if (slot.childCount > 0)
                {
                    CheckPuzzle();
                }
            }
        }
    }

    public void CheckPuzzle()
    {

        int correctSlotsCount = 0;

        for (int i = 0; i < itemSlots.Count; i++)
        {
            Transform slot = itemSlots[i];

            // Only check the slot if it hasn't been checked yet
            if (!slotChecked[i] && slot.childCount > 0)
            {
                foreach (Transform item in slot)
                {
                    int itemId = item.GetComponent<ItemValue>().itemId;
                    if (i == itemId)
                    {
                        slotChecked[i] = true;
                        Debug.Log("1 Place match");

                    }
                }
            }

            if (slotChecked[i])
            {
                correctSlotsCount++;
            }
        }

        if (correctSlotsCount == correctPlace && !isPuzzleSolved)
        {
            Debug.Log("Pintu Terbuka");
            Renderer mydoor = Door.GetComponent<Renderer>();
            mydoor.material = newDoorMaterial;
            isPuzzleSolved = true; // Set the flag to prevent further checks
            // You can add additional actions here, such as opening a door or triggering an event
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleScript : MonoBehaviour
{
    public Transform puzzleContent;
    public List<Transform> itemSlots = new List<Transform>();
    public List<Items> items = new List<Items>();
    public int correctPlace;
    public GameObject Door;
    public Material newDoorMaterial;

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

    public void Add(Items item)
    {
        items.Add(item);
    }

    public void Remove(Items item)
    {
        items.Remove(item);
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

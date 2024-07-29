using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PuzzleScript : MonoBehaviour
{
    public Transform puzzleContent;
    public List<Transform> itemSlots = new List<Transform>();
    public List<Items> items = new List<Items>();

    private bool[] slotChecked;
    private int totalRightItem = 0;

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
        foreach (Transform slot in itemSlots)
        {
            if (slot.childCount > 0)
            {
                CheckPuzzle();
            }
        }
    }

    public void CheckPuzzle()
    {
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
                        totalRightItem++;
                        slotChecked[i] = true;
                        Debug.Log("Place already match");
                    }
                }
            }
        }
    }

}

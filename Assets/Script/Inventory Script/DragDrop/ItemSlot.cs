using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    private Transform inventoryPanelTransform;
    private Transform firstPuzzleBoxTransform;
    private Transform firstArtefactPuzzleBoxTransform;

    private void Start()
    {
        inventoryPanelTransform = GameObject.Find("InventoryPuzzleCanvas/InventoryPanel/Viewport/Content")?.transform;
        firstPuzzleBoxTransform = GameObject.Find("InventoryPuzzleCanvas/FirstPuzzle/Grid")?.transform;
        firstArtefactPuzzleBoxTransform = GameObject.Find("InventoryPuzzleCanvas/FirstArtefactPuzzle/Grid")?.transform;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop dragItem = dropped.GetComponent<DragDrop>();
            dragItem.parentAfterDrag = transform;

            Items Item;
            Items getItemFromInven = InventoryManager.Instance.GetItems(dragItem.GetComponent<ItemValue>().itemId);
            Items getItemFromFirstPuzzle = PuzzleScript.Instance.GetItems(dragItem.GetComponent<ItemValue>().itemId);
            Items getItemFromFirstArtefactPuzzle = PuzzleScript.Instance.GetItems(dragItem.GetComponent<ItemValue>().itemId);


            if (getItemFromInven != null)
            {
                Item = getItemFromInven;
            }
            else if (getItemFromFirstPuzzle != null)
            {
                Item = getItemFromFirstPuzzle;
            }
            else if (getItemFromFirstArtefactPuzzle != null)
            {
                Item = getItemFromFirstArtefactPuzzle;
            }
            else
            {
                Item = null;
            }

            if (transform.parent == inventoryPanelTransform)
            {
                InventoryManager.Instance.Add(Item);
                PuzzleScript.Instance.Remove(Item);
                Debug.Log("Drop in Inventory");
            }

            if (transform.parent == firstPuzzleBoxTransform || transform.parent == firstArtefactPuzzleBoxTransform)
            {
                InventoryManager.Instance.Remove(Item);
                PuzzleScript.Instance.Add(Item);
                Debug.Log("Drop in Puzzle");
            }
        }
    }

}

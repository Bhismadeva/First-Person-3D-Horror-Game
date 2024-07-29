using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop dragItem = dropped.GetComponent<DragDrop>();
            dragItem.parentAfterDrag = transform;
        }
    }

}
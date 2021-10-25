using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearSlotUI : MonoBehaviour, IDropHandler     
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            DragDrop dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            dragDrop.droppedOnSlot = true;
            dragDrop.defaultPos = transform.position;
            dragDrop.ResetAnchor();
        }
    }

    
}

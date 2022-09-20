using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDropHandler : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        
        RectTransform inventoryPanel = transform.GetComponent<RectTransform>();
        if (!RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition))
        {
            Debug.Log("Item removed");
        }
    }
}

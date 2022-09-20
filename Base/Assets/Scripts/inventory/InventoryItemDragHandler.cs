
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : MonoBehaviour,IDragHandler,IEndDragHandler
{
    private RectTransform itemSlotContainer;
    private RectTransform itemSlotTemplate;
    private Item item;
    private void Awake()
    {
        itemSlotTemplate = transform.parent as RectTransform;
        itemSlotContainer = itemSlotTemplate.parent.parent as RectTransform;
        
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Debug.Log("Position on drag "+eventData);
        Debug.Log("Mouse position " + Input.mousePosition);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (itemSlotContainer != null)
        {
            Debug.Log("Parent is " + itemSlotContainer.gameObject.name);
            if (!RectTransformUtility.RectangleContainsScreenPoint(itemSlotContainer, Input.mousePosition))
            {
                item.OnItemDrop();
                Debug.Log("Item removed and placed on maze");
            }
            else
            {
                Debug.Log("Item removed and placed on panel");
                transform.localPosition = Vector3.zero;
            }
        }
    }

    private void Update()
    {
        Debug.Log(transform.localPosition);
    }
}

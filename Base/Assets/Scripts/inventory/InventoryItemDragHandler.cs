
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
                
                GameObject newItem = Instantiate(Resources.Load("Prefabs/Red", typeof(GameObject))) as GameObject;
                Vector3 pos =  Vector3.zero;
                pos.x += Input.mousePosition.x;
                pos.y += Input.mousePosition.y;
                Vector3.ProjectOnPlane(pos, Vector3.up);
                Debug.Log("Item removed and placed on maze" +Input.mousePosition);
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

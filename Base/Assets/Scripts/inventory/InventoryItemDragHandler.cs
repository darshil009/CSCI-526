
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

                GameObject newItem = Instantiate(InventoryResourceManager.GetPrefab(item.GetItemType())) as GameObject;
                Vector3 mousePos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
                Vector3 worldPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.origin + ray.direction * 100f, out hit))
                    worldPosition = hit.point;
                else
                    worldPosition = ray.origin + ray.direction * 100f;
                newItem.transform.position = worldPosition;
                Debug.Log("Item removed and placed on maze" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
                item.OnItemDrop();
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

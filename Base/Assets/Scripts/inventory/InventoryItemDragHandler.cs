
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private RectTransform itemSlotContainer;
    private RectTransform itemSlotTemplate;
    private RectTransform border;
    private GameObject itemObj = null;
    [SerializeField] private PlayerScript playerScript;
    private Item item;
    private void Awake()
    {
        itemSlotTemplate = gameObject.transform as RectTransform;
        itemSlotContainer = itemSlotTemplate.parent as RectTransform;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        itemObj= Instantiate(InventoryResourceManager.GetPrefab(item.GetItemType())) as GameObject;
        itemObj.transform.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("On begin drag");
    }

    private void CalculateWorldPosition()
    {


        // var a = -30 * Mathf.Deg2Rad;
        // var dir = (playerScript.transform.forward * Mathf.Cos(a) + transform.right * Mathf.Sin(a)).normalized;
        // Debug.DrawRay(playerScript.transform.position,dir*100, Color.green);

        // float pickUpDistance = 20f;
        // if(Physics.Raycast(playerScript.transform.position,dir*100,out RaycastHit raycastHit,pickUpDistance))
        // {
        //     Debug.Log("hit" +raycastHit.point);
        //     itemObj.transform.position = raycastHit.point;
        // }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("Ray origin"+ray.origin+" ray direction"+ray.direction);
        Debug.DrawRay(ray.origin, -ray.origin + -ray.direction, Color.green);

        if (Physics.Raycast(ray.origin, -ray.origin + -ray.direction * 200f, out hit)){
                print("Hit");
            Vector3 v3 = hit.point;
            v3.z +=100;
            Debug.DrawRay(ray.origin, v3, Color.black);
        }
        

    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Debug.Log("Position on drag "+eventData);
        Debug.Log("Mouse position " + Input.mousePosition);
        

        
        CalculateWorldPosition();
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("Ray origin"+ray.origin+" ray direction"+ray.direction);
        Debug.DrawRay(ray.origin, ray.origin + ray.direction, Color.green);

        if (Physics.Raycast(ray.origin, ray.origin + ray.direction * 200f, out hit)){
                print("Hit");
            Vector3 v3 = hit.point;
            v3.z +=100;
            Debug.DrawRay(ray.origin, v3, Color.black);
        }*/


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (itemSlotContainer != null)
        {
            Debug.Log("Parent is " + itemSlotContainer.gameObject.name);
            if (!RectTransformUtility.RectangleContainsScreenPoint(itemSlotContainer, Input.mousePosition))
            {

                /*GameObject newItem = Instantiate(InventoryResourceManager.GetPrefab(item.GetItemType())) as GameObject;
                Vector3 mousePos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
                Vector3 worldPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.origin + ray.direction * 100f, out hit)){
                    Debug.DrawRay(ray.origin, ray.origin + ray.direction * 100f, Color.green); print("Hit");
                    worldPosition = hit.point;
                }
                else
                    worldPosition = ray.origin + ray.direction * 100f;
                newItem.transform.position = worldPosition;
                Debug.Log("Item removed and placed on maze" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
                */
                //CalculateWorldPosition();
                itemObj.transform.GetComponent<Rigidbody>().useGravity = true;
                item.OnItemDrop();
            }
            else
            {
                Destroy(itemObj);
                itemObj=null;
                Debug.Log("Item removed and placed on panel");
                transform.localPosition = Vector3.zero;
            }
        }
    }

    private void Update()
    {
    }

}

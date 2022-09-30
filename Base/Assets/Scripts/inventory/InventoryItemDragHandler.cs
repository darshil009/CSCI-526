
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private RectTransform itemSlotContainer;
    private RectTransform itemSlotTemplate;
    private RectTransform border;
    private GameObject itemObj = null;
    private Image spriteImage = null;
    private Rigidbody rigidbody = null;
    private BoxCollider boxCollider = null;
    private Light lightComponent = null;
    [SerializeField] private PlayerScript playerScript;

    [SerializeField] private LayerMask wallsPlaneMask;
    [SerializeField] private LayerMask exceptBoxesMask;

    [SerializeField] private LayerMask playerMask;

    public event EventHandler<Item> itemDroppedFromUIEvent;

    [SerializeField] ErrorCanvas errorCanvas;
    private Item item;
    private void Awake()
    {
        itemSlotTemplate = gameObject.transform.parent as RectTransform;
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

    
    // private Vector3 ScreenToWorld(Vector3 pos)
    // {

    // }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(GameDetails.canDragFirstItem == false)return;
        itemObj= Instantiate(InventoryResourceManager.GetPrefab(item.GetItemType())) as GameObject;
        // itemObj.GetComponent<BoxCollider>().isTrigger = false;
        spriteImage = GetComponent<Image>();
        rigidbody = itemObj.transform.GetComponent<Rigidbody>();
        spriteImage.enabled = false;
        itemObj.transform.position = playerScript.transform.position;
        boxCollider =  itemObj.GetComponent<BoxCollider>();
        lightComponent = itemObj.GetComponent<Light>();
        rigidbody.useGravity = false;
    }

    private void CalculateWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        ray.origin = Camera.main.transform.position;
        if (Physics.Raycast(ray.origin,ray.direction, out hit,GameDetails.dropItemDistance,wallsPlaneMask)){
            Vector3 hitPosition = hit.point;
            Vector3 playerPosition = playerScript.transform.position;
            hitPosition.y=1;
            playerPosition.y = hitPosition.y;
            Vector3 dir = hitPosition-playerPosition;
            dir = dir.normalized;
            if(Vector3.Distance(playerPosition,hitPosition)>GameDetails.lightOnRadius){
                hitPosition = playerPosition +  dir*GameDetails.lightOnRadius;
                
            }
            else if(Vector3.Distance(playerPosition,hitPosition)>2f){
                hitPosition = playerPosition +  dir*2;
            }
            else 
            {
                hitPosition = playerPosition +  dir*1;
            }
            itemObj.transform.position = hitPosition;

        }
    }
    public void OnDrag(PointerEventData eventData)
    {      
        if(GameDetails.canDragFirstItem == false)return;
        setNoPhysics();
        //CalculateWorldPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(GameDetails.canDragFirstItem == false)return;
        
        RaycastHit hit;  
        Physics.Linecast(playerScript.transform.position, itemObj.transform.position,out hit,exceptBoxesMask);

        if (itemSlotContainer != null)
        {
            Debug.Log("hit collider "+hit.collider);
            if (!RectTransformUtility.RectangleContainsScreenPoint(itemSlotContainer, Input.mousePosition) &&
                     hit.collider == null)
            {
                setNoPhysics();
                TriggerItemDroppedFromUIEvent(item);
                itemObj.layer = LayerMask.NameToLayer("boxes");
                rigidbody.useGravity = true;

            }
            else
            {
                errorCanvas.ShowError("Cannot place the block here.");
                placeItemBackInInventoryPanel();

            }
        }
    }


    void placeItemBackInInventoryPanel()
    {
        Destroy(itemObj);
        itemObj=null;
        Debug.Log("Item removed and placed on panel");
        spriteImage.enabled = true;
        transform.localPosition = Vector3.zero;
    }
    void setNoPhysics()
    {
        // boxCollider.isTrigger = true;
        // rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    private void TriggerItemDroppedFromUIEvent(Item item)
    {
        itemDroppedFromUIEvent?.Invoke(this,item);
    }
    private void Update()
    {
        if(itemObj && GameDetails.canDragFirstItem == true){
            CalculateWorldPosition();
            setNoPhysics();
        }
    }

}

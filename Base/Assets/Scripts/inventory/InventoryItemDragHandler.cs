
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
    [SerializeField] private PlayerScript playerScript;

    [SerializeField] private LayerMask planeMask;
    [SerializeField] private LayerMask exceptBoxesMask;

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

    
    public void OnBeginDrag(PointerEventData eventData)
    {

        itemObj= Instantiate(InventoryResourceManager.GetPrefab(item.GetItemType())) as GameObject;
        // itemObj.GetComponent<BoxCollider>().isTrigger = false;
        spriteImage = GetComponent<Image>();
        rigidbody = itemObj.transform.GetComponent<Rigidbody>();
        spriteImage.enabled = false;
        itemObj.transform.position = playerScript.transform.position;
        boxCollider =  itemObj.GetComponent<BoxCollider>();

        rigidbody.useGravity = false;
        // Debug.Log("On begin drag");
    }

    private void CalculateWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        

        ray.origin = Camera.main.transform.position;
        if (Physics.Raycast(ray.origin,ray.direction, out hit,GameDetails.pickDropDistance,planeMask)){
                Vector3 position = hit.point+new Vector3(0,1,0);
                itemObj.transform.position = position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {      
        setNoPhysics();
        //CalculateWorldPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
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
        if(itemObj){
            CalculateWorldPosition();
            setNoPhysics();
        }
    }

}

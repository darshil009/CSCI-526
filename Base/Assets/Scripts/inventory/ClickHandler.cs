using UnityEngine;
using UnityEngine.UI;
using System;


public class ClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
     private RectTransform itemSlotContainer;
    private RectTransform itemSlotTemplate;
    private GameObject itemObj = null;
    private Image spriteImage = null;
    private Rigidbody rigidbody = null;
    private BoxCollider boxCollider = null;
    [SerializeField] private PlayerScript playerScript;

    [SerializeField] private LayerMask planeMask;
    [SerializeField] InventoryUI inventoryUI;



    public event EventHandler<Item> itemDroppedFromUIEvent;
    private Item item;
    Button button;

     private void Awake()
    {
        itemSlotTemplate = gameObject.transform.parent as RectTransform;
        itemSlotContainer = itemSlotTemplate.parent as RectTransform;
        button = GetComponent<Button>();
        button.onClick.AddListener(clickListener);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    private void clickListener()
    {
        itemObj= Instantiate(InventoryResourceManager.GetPrefab(item.GetItemType())) as GameObject;
        // setNoPhysics();
        spriteImage = GetComponent<Image>();
        rigidbody = itemObj.transform.GetComponent<Rigidbody>();
        //spriteImage.enabled = false;
        spriteImage.color = new Color(0,255,246,255);
        itemObj.transform.position = playerScript.transform.position + playerScript.transform.forward*2.0f;
        boxCollider =  itemObj.GetComponent<BoxCollider>();
    }

    void setNoPhysics()
    {
        boxCollider.isTrigger = true;
        rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private const float itemSlotMargin = 7f;
    [SerializeField] private InventorySpriteManager inventorySpriteManager;
    public void SetInventoryManager(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
        inventoryManager.OnItemListChanged += Inventory_OnItemListChanged;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        Refresh();
    }
    private void Awake()
    {
        Debug.Log("Init itemSlotContainer and itemSlotTemplate");
        itemSlotContainer = transform.Find("inventoryBackgroundPanel").Find("itemSlotContainer");
        Debug.Log("itemSlotContainer " + itemSlotContainer.childCount);
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        InitSlots();
    }

    private void InitSlots()
    {
       /* for (int i = 0; i < numSlots; i++)
        {
            Transform itemSlotTemplateTransform =
                Instantiate(itemSlotTemplate, itemSlotContainer);
            RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
            itemSlotTemplateRectTransform.gameObject.SetActive(false);
            itemSlotTemplateRectTransform.anchoredPosition = new Vector2(i * itemSlotSize, 0);
        }*/
    }
    private void Refresh()
    {
        
        /*Debug.Log("Destroying children "+itemSlotContainer.childCount);
        foreach(Transform child in itemSlotContainer)
        {
            
            Debug.Log("Child "+child);
            if (child != itemSlotTemplate)
            {
                
                Debug.Log("Destroying");
                Destroy(child.gameObject);
            }
        }
        */
        //Debug.Log("Destroyed children "+itemSlotContainer.childCount);
        int childCount = itemSlotContainer.childCount;
        for(int i=1;i <childCount;i++)
        {
            Transform child = itemSlotContainer.GetChild(i);
            Debug.Log("Destroying "+child);
            //child.GetComponent<RectTransform>().gameObject.SetActive(false);
            Destroy(child.gameObject);
            Debug.Log("Destroyed  ");
        }
        
        List<Item> inventory = inventoryManager.GetInventory();
        //Debug.Log("Refresing inventory with "+ inventory.Count);
        
        for(int i=0;i<inventory.Count;i++)
        {
            UpdateItemAt(inventory[i],i);
            
        }
    }

    private void UpdateItemAt(Item item,int index)
    {
        //Debug.Log("Setting active for "+(index+1));
        //Transform itemSlotTemplateTransform = this.itemSlotContainer.GetChild(index + 1);
        
        Debug.Log("Updating item at "+(index));
        Transform itemSlotTemplateTransform =
            Instantiate(itemSlotTemplate, itemSlotContainer);
        RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
        itemSlotTemplateRectTransform.gameObject.SetActive(false);
        itemSlotTemplateRectTransform.anchoredPosition = new Vector2(index * itemSlotMargin, 0);
        
        //RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
        
        itemSlotTemplateRectTransform.gameObject.SetActive(true);
        itemSlotTemplateRectTransform.Find("itemImage").GetComponent<Image>().overrideSprite =
            inventorySpriteManager.GetSprite(item);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnstackedInventoryUI : InventoryUI
{
    /*public override void InitEventHandlers()
    {
        
        inventoryManager.OnItemAdded += OnItemAdded;
        inventoryManager.OnItemChanged += OnItemChanged;
    }*/
    public override void OnItemAdded(Item item)
    {
        
        item.OnItemDropEvent += OnItemDroppedFromUI;
        Item.ItemType itemType = item.GetItemType();
        int index = itemSlotContainer.childCount-1;
        Transform itemSlotTemplateTransform =
            Instantiate(itemSlotTemplate, itemSlotContainer);
        itemToSlotTemplate[item] = itemSlotTemplateTransform;
        RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
        itemSlotTemplateRectTransform.gameObject.SetActive(true);
        
        Transform itemImageTransform =itemSlotTemplateRectTransform.Find("itemImage"); 
        itemImageTransform.GetComponent<Image>().overrideSprite =
            InventoryResourceManager.GetSprite(itemType);
        itemImageTransform.GetComponent<InventoryItemDragHandler>().SetItem(item);
        itemSlotTemplateRectTransform.Find("itemCount").GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
    }


    private void OnItemDroppedFromUI(object sender, Item item)
    {
        Debug.Log("destroying slot template in UI");
        Destroy(itemToSlotTemplate[item].gameObject);
    }
    public override void OnItemChanged(Tuple<Item,int,int> tuple)
    {
        OnItemRemoved(tuple);

    }

    private void OnItemRemoved(Tuple<Item, int, int> tuple)
    {
        int index = tuple.Item2 + 1;
        Destroy(itemSlotContainer.GetChild(index).gameObject);
    }
}




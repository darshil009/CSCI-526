using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StackedInventoryUI : InventoryUI
{ 
    /*public override void InitEventHandlers()
    {
        Debug.Log("Setting event handlers");
        inventoryManager.OnItemAdded += OnItemAdded;
        inventoryManager.OnItemChanged += OnItemChanged;
    }*/
    public override void OnItemAdded(Item item)
    {
        int index = itemSlotContainer.childCount-1;
        Item.ItemType itemType = item.GetItemType();
        Transform itemSlotTemplateTransform =
            Instantiate(itemSlotTemplate, itemSlotContainer);
        RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
        itemSlotTemplateRectTransform.gameObject.SetActive(true);
        itemSlotTemplateRectTransform.Find("itemImage").GetComponent<Image>().overrideSprite =
            InventoryResourceManager.GetSprite(itemType);
        itemSlotTemplateRectTransform.Find("itemCount").GetComponent<TextMeshProUGUI>().SetText(""+1);
    }
    
    
    
    public override void OnItemChanged(Tuple<Item,int,int> tuple)
    {
        Item item = tuple.Item1;
        Item.ItemType itemType = item.GetItemType();
        int index = tuple.Item2+1;
        int count = tuple.Item3;
        if (count != 0)
        {
            Transform itemSlotTemplateTransform = itemSlotContainer.GetChild(index);
            RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
            itemSlotTemplateRectTransform.Find("itemCount").GetComponent<TextMeshProUGUI>().SetText("" + count);
        }
        else
        {
            int childCount = itemSlotContainer.childCount;
            Destroy(itemSlotContainer.GetChild(index).gameObject);
        }
    }

}



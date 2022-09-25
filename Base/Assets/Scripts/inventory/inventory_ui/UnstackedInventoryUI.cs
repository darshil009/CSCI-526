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
    protected override void OnItemAddedInList(object sender, Item item)
    {
        

        //Call item pick up to nofify other interested parties about the addition to inventory
        //item.OnItemPickUp();
        
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
        itemImageTransform.GetComponent<InventoryItemDragHandler>().itemDroppedFromUIEvent += OnItemDroppedFromUI;

        itemImageTransform.GetComponent<ClickHandler>().SetItem(item);
        itemImageTransform.GetComponent<ClickHandler>().itemDroppedFromUIEvent += OnItemDroppedFromUI;

        itemSlotTemplateRectTransform.Find("itemCount").GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
    }


    private void OnItemDroppedFromUI(object sender, Item item)
    {
        Destroy(itemToSlotTemplate[item].gameObject);
        itemToSlotTemplate.Remove(item);
        OnItemRemovedFromUI(item);
    }
    public override void OnItemChangedInList(object sender, Tuple<Item,int,int> tuple)
    {
        OnItemRemoved(tuple);

    }

    private void OnItemRemoved(Tuple<Item, int, int> tuple)
    {
        int index = tuple.Item2 + 1;
        Destroy(itemSlotContainer.GetChild(index).gameObject);
    }
}




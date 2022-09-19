using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline.Actions;
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
        inventoryManager.OnItemAdded += OnItemAdded;
        inventoryManager.OnItemChanged += OnItemChanged;
    }

    private void OnItemAdded(object sender, Item.ItemType itemType)
    {
        int index = itemSlotContainer.childCount-1;
        Transform itemSlotTemplateTransform =
            Instantiate(itemSlotTemplate, itemSlotContainer);
        RectTransform itemSlotTemplateRectTransform = itemSlotTemplateTransform.GetComponent<RectTransform>();
        itemSlotTemplateRectTransform.anchoredPosition = new Vector2(index * itemSlotMargin, 0);
        itemSlotTemplateRectTransform.gameObject.SetActive(true);
        itemSlotTemplateRectTransform.Find("itemImage").GetComponent<Image>().overrideSprite =
            inventorySpriteManager.GetSprite(itemType);
        itemSlotTemplateRectTransform.Find("itemCount").GetComponent<TextMeshProUGUI>().SetText(""+1);
    }
    
    
    
    private void OnItemChanged(object sender, Tuple<Item.ItemType,int,int> tuple)
    {
        Item.ItemType itemType = tuple.Item1;
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
            for (int i = index; i < childCount; i++)
            {
                itemSlotContainer.GetChild(i).GetComponent<RectTransform>().anchoredPosition -=
                    new Vector2(itemSlotMargin, 0);
            }
        }
    }
    
    private void Awake()
    {
        Debug.Log("Init itemSlotContainer and itemSlotTemplate");
        itemSlotContainer = transform.Find("inventoryBackgroundPanel").Find("itemSlotContainer");
        Debug.Log("itemSlotContainer " + itemSlotContainer.childCount);
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }
}

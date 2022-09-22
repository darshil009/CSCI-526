using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryUI : MonoBehaviour
{
    protected InventoryManager inventoryManager;
    protected Transform itemSlotContainer;
    protected Transform itemSlotTemplate;
    protected Dictionary<Item, Transform> itemToSlotTemplate;

    public abstract void OnItemAdded( Item item);
    public abstract void OnItemChanged(Tuple<Item, int, int> tuple);
    public void SetInventoryManager(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
        InitEventHandlers();
    }
    private void Awake()
    {
        // Debug.Log("Init itemSlotContainer and itemSlotTemplate");
        itemSlotContainer = transform.Find("inventoryBackgroundPanel").Find("itemSlotContainer");
        // Debug.Log("itemSlotContainer " + itemSlotContainer.childCount);
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemToSlotTemplate = new Dictionary<Item, Transform>();
    }

    //public abstract void InitEventHandlers();
    protected virtual void InitEventHandlers()
    {
        //inventoryManager.OnItemAdded += OnItemAdded;
        //inventoryManager.OnItemChanged += OnItemChanged;
    }

}

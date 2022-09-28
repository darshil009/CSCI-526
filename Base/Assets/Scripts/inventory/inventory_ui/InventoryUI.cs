using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryUI : MonoBehaviour
{
    protected InventoryManager inventoryManager;
    protected Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    protected Dictionary<Item, Transform> itemToSlotTemplate;

    protected abstract void OnItemAddedInList( object sender, Item item);
    public abstract void OnItemChangedInList(object sender, Tuple<Item, int, int> tuple);

    public event EventHandler<Item> OnItemRemovedFromUIEvent;

    public void SetInventoryManager(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
        inventoryManager.itemAddedEvent += OnItemAddedInList;
    }

    protected void OnItemRemovedFromUI(Item item)
    {
        OnItemRemovedFromUIEvent?.Invoke(this,item);
    }
    private void Awake()
    {
        itemSlotContainer = transform.Find("inventoryBackgroundPanel").Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemToSlotTemplate = new Dictionary<Item, Transform>();
    }

}

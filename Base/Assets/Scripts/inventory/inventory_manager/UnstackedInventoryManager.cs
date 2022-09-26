using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnStackedInventoryManager:InventoryManager
{

    private LinkedList<Item> items;
    public UnStackedInventoryManager()
    {
        itemOrder = new LinkedList<Item.ItemType>();
        items = new LinkedList<Item>();
    }

    public override void AddItem(Item item)
    {

        //item.OnItemDropEvent += OnItemDroppedFromUI;
        itemOrder.AddLast(item.GetItemType());
        items.AddLast(item);
        OnItemAddProcessed(item);
    }

    protected override void OnItemRemovedFromUI(object sender, Item item)
    {
        // Debug.Log("Item removed from inventory");
        items.Remove(item);
    }
}
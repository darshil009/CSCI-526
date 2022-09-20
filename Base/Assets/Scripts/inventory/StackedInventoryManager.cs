using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackedInventoryManager:InventoryManager
{
    
    private Dictionary<Item.ItemType, LinkedList<Item>> items;
    public StackedInventoryManager()
    {
        items = new Dictionary<Item.ItemType, LinkedList<Item>>();
        itemOrder = new LinkedList<Item.ItemType>();

    }

    public override void AddItem(Item item)
    {
        
        if (!items.ContainsKey(item.GetItemType()))
        {
            itemOrder.AddLast(item.GetItemType());
            items[item.GetItemType()]= new LinkedList<Item>();
            items[item.GetItemType()].AddLast(item);
            OnItemAddProcessed(item);

        }
        else
        {
            items[item.GetItemType()].AddLast(item);
            int index = itemOrder.TakeWhile(n => n != item.GetItemType()).Count();
            OnItemChangeProcessed(item,index,items[item.GetItemType()].Count);
        }

        Debug.Log("Item added");
        
        
    }

    public override void RemoveItem(Item item)
    {
        Debug.Log("remove item");
        Item.ItemType itemType = item.GetItemType();
        if (!items.ContainsKey(itemType))
            return;

        items[itemType].Remove(item);
        int remaining = items[itemType].Count;
        int index = itemOrder.TakeWhile(n => n != itemType).Count();
        if (remaining == 0)
        {
            
            itemOrder.Remove(itemType);
            items.Remove(itemType);
        }
        OnItemChangeProcessed(item,index,remaining);
        
    }
}
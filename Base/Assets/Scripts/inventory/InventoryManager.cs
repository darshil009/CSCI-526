using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager
{
    private Dictionary<Item.ItemType, int> items;
    private LinkedList<Item.ItemType> itemOrder; 
    public event EventHandler<Item.ItemType> OnItemAdded;
    public event EventHandler<Tuple<Item.ItemType,int,int>> OnItemChanged;

    public InventoryManager()
    {
        items = new Dictionary<Item.ItemType, int>();
        itemOrder = new LinkedList<Item.ItemType>();

    }

    public void AddItem(Item.ItemType itemType)
    {
        if (!items.ContainsKey(itemType))
        {
            itemOrder.AddLast(itemType);
            items.Add(itemType, 1);
            OnItemAdded?.Invoke(this,itemType);

        }
        else
        {
            items[itemType]++;
            int index = itemOrder.TakeWhile(n => n != itemType).Count();
            OnItemChanged?.Invoke(this,new Tuple<Item.ItemType, int,int>(itemType,index,items[itemType]));
        }

        Debug.Log("Item added");
        
        
    }

    public void RemoveItem(Item.ItemType itemType)
    {
        Debug.Log("remove item");
        if (!items.ContainsKey(itemType))
            return;

        items[itemType]-=1;
        int remaining = items[itemType];
        int index = itemOrder.TakeWhile(n => n != itemType).Count();
        if (remaining == 0)
        {
            
            itemOrder.Remove(itemType);
            items.Remove(itemType);
        }
            OnItemChanged?.Invoke(this,new Tuple<Item.ItemType, int, int>(itemType,index,remaining));
        
    }
    public LinkedList<Item.ItemType> GetItemOrder()
    {
        return itemOrder;
    }
}

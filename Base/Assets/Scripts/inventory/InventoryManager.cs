using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InventoryManager
{

    
    protected LinkedList<Item.ItemType> itemOrder;
    public event EventHandler<Item> OnItemAdded;
    public event EventHandler<Tuple<Item,int,int>> OnItemChanged;
    public InventoryUI inventoryUI;


    public abstract void AddItem(Item item);

    public abstract void RemoveItem(Item item);
    public LinkedList<Item.ItemType> GetItemOrder()
    {
        return itemOrder;
    }

    public void SetInventoryUI(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }
    protected virtual void OnItemAddProcessed(Item item)
    {
        OnItemAdded?.Invoke(this,item);
    }
    protected virtual void OnItemChangeProcessed(Item item,int index, int count)
    {
        OnItemChanged?.Invoke(this,new Tuple<Item, int,int>(item,index,count));
    }
}


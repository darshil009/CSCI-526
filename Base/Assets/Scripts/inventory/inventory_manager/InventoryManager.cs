using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InventoryManager
{

    
    protected LinkedList<Item.ItemType> itemOrder;
    public event EventHandler<Item> itemAddedEvent;
    public event EventHandler<Tuple<Item,int,int>> itemChangedEvent;
    public InventoryUI inventoryUI;


    public abstract void AddItem(Item item);
    public LinkedList<Item.ItemType> GetItemOrder()
    {
        return itemOrder;
    }

    public void SetInventoryUI(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
        inventoryUI.OnItemRemovedFromUIEvent += OnItemRemovedFromUI;
    }
    protected virtual void OnItemAddProcessed(Item item)
    {
        itemAddedEvent?.Invoke(this,item);
    }
    protected virtual void OnItemChangeProcessed(Item item,int index, int count)
    {
        itemChangedEvent?.Invoke(this,new Tuple<Item, int,int>(item,index,count));
    }

     protected abstract void OnItemRemovedFromUI(object sender, Item item);
}


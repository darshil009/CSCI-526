using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager
{
    private List<Item> inventory;
    public event EventHandler OnItemListChanged;

    public InventoryManager()
    {
        inventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log("Item added");
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
        
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }
}

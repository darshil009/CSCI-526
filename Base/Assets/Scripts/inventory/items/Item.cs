using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Block1LB,
        Block2LB,
        Block3LB,
        Block5LB,
        
    }

    public static ItemType[] GetItemTypes()
    {
        return (ItemType[])Enum.GetValues(typeof(ItemType));
    }
    protected ItemType itemType;

    public ItemType GetItemType()
    {
        return itemType;
    }

    public event EventHandler<Item> OnItemDropEvent;

    public void OnItemDrop()
    {
        OnItemDropEvent?.Invoke(this,this);
        GameDetails.currentTotalWeight -= GameDetails.weights[this.GetItemType()];

    }

    public Item(ItemType itemType)
    {
        this.itemType = itemType;
    }

}

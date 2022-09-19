using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Block
    }
    
    protected ItemType itemType;

    public ItemType GetItemType()
    {
        return itemType;
    }
}

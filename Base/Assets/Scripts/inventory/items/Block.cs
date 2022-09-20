using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Item
{
    private readonly int weight;

    protected Block(int weight,ItemType itemType):base(itemType)
    {
        this.weight = weight;
    }
    public int GetWeight()
    {
        return weight;
    }
}

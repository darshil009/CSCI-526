using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Item
{
    private readonly float weight;

    protected Block(float weight,ItemType itemType):base(itemType)
    {
        this.weight = weight;
    }
    public float GetWeight()
    {
        return weight;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Item
{
    private readonly int weight;

    protected Block(int weight)
    {
        this.weight = weight;
    }
    public int GetWeight()
    {
        return weight;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Item
{
    protected int weight;

    protected Block(int weight)
    {
        this.weight = weight;
    }
    public int getWeight()
    {
        return weight;
    }
}

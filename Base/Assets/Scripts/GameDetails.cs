using System.Collections.Generic;

public struct GemsCount
{
    public int Green;
    public int Red;
    public int Blue;
}

public static class GameDetails {
    public static readonly string BlueGemsTag = "blue_gems";
    public static readonly string RedGemsTag = "red_gems";
    public static readonly string GreenGemsTag = "green_gems";
    public static readonly string SpikeBallTag = "SPIKE_BALL";
    public static readonly int EnemyVisionRadius = 5;
    public static float currentTotalWeight = 0;
    public static GemsCount NumGems;
    public static Dictionary<Item.ItemType, int> weights = new Dictionary<Item.ItemType, int>()
    {
        {
            Item.ItemType.Block1LB, 1
        },
        {
            Item.ItemType.Block2LB, 2
        },
        {
            Item.ItemType.Block3LB, 3
        },
        {
            Item.ItemType.Block5LB, 5
        }
    };
}
using System.Collections.Generic;

public struct GemsCount
{
    public int Green;
    public int Red;
    public int Blue;
}

public static class GameDetails {
    // public static readonly string BlueGemsTag = "blue_gems";
    // public static readonly string RedGemsTag = "red_gems";
    // public static readonly string GreenGemsTag = "green_gems";
    // public static readonly string SpikeBallTag = "SPIKE_BALL";
    public static readonly int EnemyVisionRadius = 6;
    public static float currentTotalWeight = 0;
    public static GemsCount NumGems;
    public static float dropItemDistance = 125f;
    public static float pickItemDistance = 12.5f;

    public static float lightOnRadius = 3f;


    public static bool firstLight = true;
    public static bool pause = false;
    public static bool firstItemPickedUp = false;
    public static bool firstItemDropped = false;
    public static bool tutorialEnded = false;
    public static bool canDragFirstItem = false;
    
    public static Dictionary<Item.ItemType, float> weights = new Dictionary<Item.ItemType, float>()
    {
        {
            Item.ItemType.Block05LB, 0.5f
        },
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
    
    public static Dictionary<string, float> weights_from_tag = new Dictionary<string, float>()
    {
        
        {
            "1lb", 1
        },
        {
            "2lb", 2
        },
        {
            "3lb", 3
        },
        {
            "5lb", 5
        }
    };

    public static void reset(){
        firstLight = true;
        pause = false;
        firstItemPickedUp = false;
        firstItemDropped = false;
        tutorialEnded = false;
        canDragFirstItem = false;
    }
}
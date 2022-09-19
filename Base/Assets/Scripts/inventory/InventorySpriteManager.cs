using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpriteManager : MonoBehaviour
{

    [SerializeField] private Sprite[] blockSprites; 


    public Sprite GetSprite(Item item)
    {
        switch (item.GetItemType())
        {
            case Item.ItemType.Block:
                Block block = (Block)item;
                switch (block.getWeight())
                {
                    case 1:
                        return blockSprites[0];
                    case 2:
                        return blockSprites[1];
                    case 3:
                        return blockSprites[2];
                    case 5:
                        return blockSprites[3];
                    default:
                        return blockSprites[0];
                }
                
        }

        return blockSprites[0];
    }
}

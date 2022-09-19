using UnityEngine;

public class InventorySpriteManager : MonoBehaviour
{

    [SerializeField] private Sprite[] blockSprites; 


    public Sprite GetSprite(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Block1LB:
                return blockSprites[0];
            case Item.ItemType.Block2LB:
                return blockSprites[1];
            case Item.ItemType.Block3LB:
                return blockSprites[2];
            case Item.ItemType.Block5LB:
                return blockSprites[3];
            default:
                return blockSprites[0];

        }
    }
}

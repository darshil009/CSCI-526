using UnityEngine;

public class InventorySpriteManager : MonoBehaviour
{

    [SerializeField] private Sprite[] blockSprites; 


    public Sprite GetSprite(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Block1LB:
                return Resources.Load<Sprite>("Inventory/blocks/1lb");
            case Item.ItemType.Block2LB:
                return Resources.Load<Sprite>("Inventory/blocks/2lb");
            case Item.ItemType.Block3LB:
                return Resources.Load<Sprite>("Inventory/blocks/3lb");
            case Item.ItemType.Block5LB:
                return Resources.Load<Sprite>("Inventory/blocks/5lb");
            default:
                return Resources.Load<Sprite>("Inventory/blocks/1lb");

        }
    }
}

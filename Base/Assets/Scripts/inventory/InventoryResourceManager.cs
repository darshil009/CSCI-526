using UnityEngine;

public class InventoryResourceManager : MonoBehaviour
{
 

  private const string SpritePath = "Sprites/Inventory/";
  private const string PrefabPath = "Prefabs/";
    
    public static Sprite GetSprite(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Block1LB:
                return Resources.Load<Sprite>(SpritePath +"blocks/1lb");
            case Item.ItemType.Block2LB:
                return Resources.Load<Sprite>(SpritePath +"blocks/2lb");
            case Item.ItemType.Block3LB:
                return Resources.Load<Sprite>(SpritePath +"blocks/3lb");
            case Item.ItemType.Block5LB:
                return Resources.Load<Sprite>(SpritePath +"blocks/5lb");
            default:
                return Resources.Load<Sprite>(SpritePath +"blocks/1lb");

        }
    }
    
    public static Object GetPrefab(Item.ItemType itemType)
    {
        //return Resources.Load(PrefabPath+"1lbBlock");
        switch (itemType)
        {
            case Item.ItemType.Block1LB:
                Debug.Log("Path is "+PrefabPath + "Block1LB");
                return Resources.Load(PrefabPath+"Block1LB");
            case Item.ItemType.Block2LB:
                return Resources.Load(PrefabPath+"Block2LB");
            case Item.ItemType.Block3LB:
                return Resources.Load(PrefabPath+"Block3LB");
            case Item.ItemType.Block5LB:
                return Resources.Load(PrefabPath+"Block5LB");
            default:
                return Resources.Load(PrefabPath+"Block1LB");

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    

    //void Update()
    //{
    //    transform.Rotate(0f, 90* Time.deltaTime, 0f, Space.Self);
    //}
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript!=null)
        {
            if (CompareTag( "1lb"))
            {
                playerScript.getInventoryManager().AddItem(Item.ItemType.Block1LB);
                playerScript.decreaseSpeed(1);
            }
            else if (CompareTag( "2lb"))
            {
                playerScript.getInventoryManager().AddItem(Item.ItemType.Block2LB);
                playerScript.decreaseSpeed(2);
            }
            else if (CompareTag( "3lb"))
            {
                playerScript.getInventoryManager().AddItem(Item.ItemType.Block3LB);
                playerScript.decreaseSpeed(3);
            }
            else if (CompareTag( "5lb"))
            {
                playerScript.getInventoryManager().AddItem(Item.ItemType.Block5LB);
                playerScript.decreaseSpeed(5);
            }
        }
        Destroy(gameObject);
    }
}

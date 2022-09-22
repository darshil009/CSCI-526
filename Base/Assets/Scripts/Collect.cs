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
                playerScript.getInventoryManager().AddItem(new Block1());
                GameDetails.currentTotalWeight += 1;
                Destroy(gameObject);
            }
            else if (CompareTag( "2lb"))
            {
                playerScript.getInventoryManager().AddItem(new Block2());
                GameDetails.currentTotalWeight += 2;
                Destroy(gameObject);
                
            }
            else if (CompareTag( "3lb"))
            {
                playerScript.getInventoryManager().AddItem(new Block3());
                GameDetails.currentTotalWeight += 3;
                Destroy(gameObject);
            }
            else if (CompareTag( "5lb"))
            {
                playerScript.getInventoryManager().AddItem(new Block5());
                GameDetails.currentTotalWeight += 5;
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
   public PlayerBehavior player;
    private Transform doorTransform;
    bool isDoorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        doorTransform = transform.Find("01_low"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(player.getStarsCollected() == GameConstants.TOTAL_STARS && !isDoorOpen) {
            print("door: all clues collected");
            GetComponent<BoxCollider>().enabled = false;
            doorTransform.Rotate(0f, 0f, -90f, Space.Self);
            isDoorOpen = true;
        }

    }
}

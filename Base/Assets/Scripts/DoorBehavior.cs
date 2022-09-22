using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    private Transform doorTransform;
    bool isDoorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        doorTransform = transform.Find("01_low"); 
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(GameDetails.NumGems.Green == 1 && !isDoorOpen) {
    //        print("door: all clues collected");
    //        doorTransform.Rotate(0f, 0f, -90f, Space.Self);
    //        isDoorOpen = true;
    //    }

    //}

    public void changeDoor()
    {
        if (!isDoorOpen)
        {
            print("door: all clues collected");
            doorTransform.Rotate(0f, 0f, -90f, Space.Self);
            isDoorOpen = true;
        }

    }
}
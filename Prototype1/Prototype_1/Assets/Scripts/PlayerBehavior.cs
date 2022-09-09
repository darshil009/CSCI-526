using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCam;
    private float PLAYER_SPEED = 10.0f;
    private int starsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
    }

    void updateMovement(){

        float directionX = Input.GetAxisRaw("Horizontal");
        float directionZ = Input.GetAxisRaw("Vertical");
        
        Vector3 direction  = new Vector3(directionX, 0, directionZ).normalized;
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * Time.deltaTime * PLAYER_SPEED);
            
        }

    }

    public int getStarsCollected(){
        return starsCollected;
    }

    
    void OnTriggerEnter(Collider c){
        print("player: collision");
        if (c.tag == GameConstants.STAR_TAG) {
            starsCollected++;
            print("clues collected: " + starsCollected);
        }
    }
}

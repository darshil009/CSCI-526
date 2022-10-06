using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPlaneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.decreaseHealth(100);
        }
    }
}

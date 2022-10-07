using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlLavaCameras : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject lavaRoomCamera;
    [SerializeField] private float secondsToWait;


    private IEnumerator SwitchCameras(PlayerScript ps)
    {
        yield return new WaitForSeconds(secondsToWait);
        mainCamera.SetActive(true);
        lavaRoomCamera.SetActive(false);
        yield return new WaitForSeconds(1);
        ps.stopMovement = false;
        GameDetails.isLavaRoomCameraActive = false;
        Destroy(transform.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.stopMovement = true;
            GameDetails.isLavaRoomCameraActive = true;
            mainCamera.SetActive(false);
            lavaRoomCamera.SetActive(true);
            StartCoroutine(SwitchCameras(playerScript));
        }
    }
}

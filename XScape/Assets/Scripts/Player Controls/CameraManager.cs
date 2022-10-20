using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;

    private int activeCamera = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            activeCamera = 1 - activeCamera;
            if (activeCamera == 0)
            {
                firstPersonCamera.gameObject.SetActive(false);
                thirdPersonCamera.gameObject.SetActive(true);
            }
            else
            {
                thirdPersonCamera.gameObject.SetActive(false);
                firstPersonCamera.gameObject.SetActive(true);
            }
            
        }
        
    }
}

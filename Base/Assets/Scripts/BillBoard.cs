using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    [SerializeField] public Transform camera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}

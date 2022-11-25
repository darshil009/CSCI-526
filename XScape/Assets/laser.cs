using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject explosionPrefab;

    public static bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider) lr.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("MagnetBlock") || hit.transform.CompareTag("MagnetBlock1"))
            {
                Instantiate(explosionPrefab, hit.transform.position, Quaternion.identity);
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("cap1") || hit.transform.CompareTag("vp") || hit.transform.CompareTag("NonMagnetBlock")) 
                return;

            else
            {
                lr.SetPosition(1, endPoint.position);
            }
            
        }
    }
}

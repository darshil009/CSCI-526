using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject explosionPrefab;
    AudioSource audioData;
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
        
        RaycastHit[] hits;
        var dist = Vector3.Distance(startPoint.position, endPoint.position);
        hits = Physics.RaycastAll(transform.position, transform.forward, dist);
        System.Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));
        foreach (var hit in hits)
        {
            lr.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("MagnetBlock") || hit.transform.CompareTag("MagnetBlock1"))
            {
                audioData = GetComponent<AudioSource>();

                Instantiate(explosionPrefab, hit.transform.position, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                audioData.Play(0);
                return;
            }
            if (hit.transform.CompareTag("cap1") || hit.transform.CompareTag("vp") || hit.transform.CompareTag("NonMagnetBlock")) 
                return;

            lr.SetPosition(1, endPoint.position);
            
        }
    }
}

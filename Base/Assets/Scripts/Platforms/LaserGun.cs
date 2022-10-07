using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    LineRenderer lineRenderer;
    private Transform laserSourcePoint;
    private float laserDuration;
    private float range = 100f;
    // Start is called before the first frame update
    private Transform src,des;
    [SerializeField] private PlayerScript playerScript;

    [SerializeField] private LayerMask playerMask;
    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        src = transform.Find("src");
        des = transform.Find("des");
        range = Vector3.Distance(src.position,des.position);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0,src.position);
        lineRenderer.SetPosition(1,des.position);
        RaycastHit hit;
        Debug.DrawLine(src.position,des.position,Color.green,5f);
        
        if(Physics.Linecast(src.position,des.position,playerMask))
        {    
                Debug.Log("In here");
                playerScript.decreaseHealth(20);
        }
    }
}

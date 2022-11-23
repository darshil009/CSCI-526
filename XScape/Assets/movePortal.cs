using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePortal : MonoBehaviour
{
    [SerializeField] LayerMask portalButtonMask;
    //Rigidbody rb;
    [SerializeField] Rigidbody rb;
    [SerializeField, Range(-1, 1)] private int XAxis;
    [SerializeField, Range(-1, 1)] private int YAxis;
    [SerializeField, Range(-1, 1)] private int ZAxis;
    [SerializeField] float speed= 2f;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    MeshRenderer meshRenderer;
    private bool isActive;
    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = inactiveMaterial;
        isActive = false;
    }
    private void Start()
    {

        //pm = GameObject.FindGameObjectWithTag("MovingPortal").GetComponent<portalMovement>();
        
    }

    //public void leftClick()
    //{
        
    //    addForce();
    //}
    //public void rightClick()
    //{

    //    addForce();
    //}
    public void Activate()
    {
        this.isActive = true;
        meshRenderer.material = activeMaterial;
        addForce();
    }
    public void Deactivate()
    {
        this.isActive = false;
        meshRenderer.material = inactiveMaterial;
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void addForce()
    {
        //rb.AddForce(new Vector3(5, 5, 5));
        rb.velocity = new Vector3(0,0,0);
    
        rb.AddForce(new Vector3(XAxis*speed,YAxis*speed,ZAxis*speed), ForceMode.VelocityChange);

    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        ray.origin = Camera.main.transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f, portalButtonMask))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log(gameObject.name);
                    if (gameObject.CompareTag("LeftButtonPortal"))
                    {
                        if (this.isActive)
                        {
                            Deactivate();
                        }
                        else Activate();
                        //leftClick();
                    }
                    
                    else if (gameObject.CompareTag("RightButtonPortal"))
                    {
                        if (this.isActive)
                        {
                            Deactivate();
                        }
                        else Activate();
                        //rightClick();
                    }
                }
            }
        }
    }
}

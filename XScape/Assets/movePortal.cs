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
    //[SerializeField] Material otherInactiveMaterial;
    [SerializeField] List<GameObject> otherButtons;
    private List<movePortal> mps=new List<movePortal>();
    //[SerializeField] List<List<GameObject>> buttons;
    //[SerializeField] Material leftActiveMaterial;
    //[SerializeField] Material leftInActiveMaterial;
    //[SerializeField] Material rightActiveMaterial;
    //[SerializeField] Material rightInActiveMaterial;

    MeshRenderer meshRenderer;
    public bool isActive;
    //private bool leftActive=false;
    //private bool rightActive=false;
    

    private void Start()
    {
        foreach (var button in otherButtons)
        {
            mps.Add(button.GetComponent<movePortal>());
        }
        //mp = otherButton.GetComponent<movePortal>();

        //pm = GameObject.FindGameObjectWithTag("MovingPortal").GetComponent<portalMovement>();
        
    }

   
    public void Activate()
    {
        foreach (var mp in mps)
        {
            mp.Deactivate();
        }
      
        //otherButton.GetComponent<MeshRenderer>().material = otherInactiveMaterial;
     
        gameObject.GetComponent<MeshRenderer>().material = activeMaterial;


        addForce();
    }
    public void Deactivate()
    {
        gameObject.GetComponent<MeshRenderer>().material = inactiveMaterial;

        isActive = false;
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void DeactivateAll()
    {

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
                  
                    if (isActive)
                    {

                        
                        Deactivate();
                    }
                    else
                    {
                        //Debug.Log(isActive + " " + mp.isActive);
                        isActive = true;
                        Activate();
                    }
                    //if (gameObject.CompareTag("LeftButtonPortal"))
                    //{
                    //    if (this.leftActive)
                    //    {
                    //        Deactivate();
                    //    }
                    //    else Activate();
                    //    //leftClick();
                    //}
                    
                    //else if (gameObject.CompareTag("RightButtonPortal"))
                    //{
                    //    if (this.rightActive)
                    //    {
                    //        Deactivate();
                    //    }
                    //    else Activate();
                    //    //rightClick();
                    //}
                }
            }
        }
    }
}

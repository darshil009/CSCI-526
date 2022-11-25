using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMagnetButtonController : MonoBehaviour
{
    private bool isActive;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    [SerializeField] LayerMask nonMagnetButtonMask;

    private GameObject quadSouth; 
    private GameObject quadNorth; 
    private GameObject quadWest; 
    private GameObject quadEast; 

    float forceStrength = 0.3f;
    private float secondsToApplyForce = 10;
    private float timeCount = 0;
    private Vector3 directionToApplyForce = Vector3.zero;
    private bool isPushing = false;
    

    private void Start()
    {
        quadSouth = transform.Find("QuadSouth").gameObject;
        quadNorth = transform.Find("QuadNorth").gameObject;
        quadWest = transform.Find("QuadWest").gameObject;
        quadEast = transform.Find("QuadEast").gameObject;

        SetMaterialAll(inactiveMaterial);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        ray.origin = Camera.main.transform.position;
        //  Debug.DrawRay(ray.origin,ray.direction*100,Color.green);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f, nonMagnetButtonMask))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Vector3 pushDirection = hit.normal - Vector3.up;
                    Vector3 direction = GetHitFace(pushDirection);
                    if(!isPushing && !Vector3.zero.Equals(direction)  && !Vector3.up.Equals(direction) &&  !Vector3.down.Equals(direction)){
                        isPushing = true;
                        timeCount = 0;
                        directionToApplyForce = -direction;
                        SetMaterial(activeMaterial, directionToApplyForce);
                    }
                }
            }
        }else{
            isPushing = false;
            SetMaterial(inactiveMaterial, directionToApplyForce);
            directionToApplyForce = Vector3.zero;
        }
    }

    private void FixedUpdate(){
       if(isPushing)
            GetComponent<Rigidbody>().AddForce(directionToApplyForce * forceStrength * Time.deltaTime*500-GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
       
    }

    Vector3 GetHitFace(Vector3 incomingVec)
     {
         //Vector3 incomingVec = hit.normal - Vector3.up;
 
         if (incomingVec == new Vector3(0, -1, -1))
             return new Vector3(0, 0, -1); //return Face.South; 
 
         if (incomingVec == new Vector3(0, -1, 1))
             return new Vector3(0, 0, 1); //return Face.North;
 
         if (incomingVec == new Vector3(0, 0, 0))
             return new Vector3(0, 1, 0); //return Face.Up;
 
         if (incomingVec == new Vector3(1, 1, 1))
             return new Vector3(0, -1, 0); //return Face.Down;
 
         if (incomingVec == new Vector3(-1, -1, 0))
             return new Vector3(-1, 0, 0); //return Face.West;
 
         if (incomingVec == new Vector3(1, -1, 0))
             return new Vector3(1, 0, 0); //return Face.East;
 
         return Vector3.zero;//Face.None;
     }
     
      private void SetMaterial(Material material, Vector3 direction){
           if(direction == Vector3.forward)
                quadSouth.GetComponent<Renderer>().material = material;

            else if (direction == Vector3.back)
                quadNorth.GetComponent<Renderer>().material = material;

            else if (direction ==  Vector3.left)
                quadEast.GetComponent<Renderer>().material = material;

            else if (direction ==  Vector3.right)
                quadWest.GetComponent<Renderer>().material = material;
               
    }

    private void SetMaterialAll(Material material){
            quadSouth.GetComponent<Renderer>().material = material;
            quadNorth.GetComponent<Renderer>().material = material;
            quadWest.GetComponent<Renderer>().material = material;
            quadEast.GetComponent<Renderer>().material = material;

    }
}

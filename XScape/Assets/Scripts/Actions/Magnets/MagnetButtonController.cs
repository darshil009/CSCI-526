using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButtonController : MonoBehaviour
{
    private bool isActive;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;

    [SerializeField] LayerMask magnetButtonMask;

    [SerializeField] private List<GameObject> magnetBlocks;

    private List<Rigidbody> blocksRigidBodies;
    [SerializeField] float forceStrength = 0.5f;
    [SerializeField] MagnetButtonManager magnetButtonManager;
    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = inactiveMaterial;
        blocksRigidBodies = new List<Rigidbody>();
        foreach(GameObject gameObject in magnetBlocks)
        {
            blocksRigidBodies.Add(gameObject.GetComponent<Rigidbody>());
        }
        isActive = false;
    }

    public void Activate()
    {
        // Debug.Log("Deactivating all");
        magnetButtonManager.DeactivateAll();
        this.isActive = true;
        meshRenderer.material = activeMaterial;


    }

    public void Deactivate()
    {
        this.isActive = false;
        meshRenderer.material = inactiveMaterial;
        foreach (Rigidbody rigidbody in blocksRigidBodies)
        {
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
                
        }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        ray.origin = Camera.main.transform.position;
        //  Debug.DrawRay(ray.origin,ray.direction*100,Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f, magnetButtonMask))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (this.isActive)
                    {
                        Deactivate();
                    }
                    else Activate();
                }
            }
        }
    }

    private void FixedUpdate()
    {


        if (this.isActive)
        {
            foreach (Rigidbody rigidbody in blocksRigidBodies)
            {
                if (rigidbody != null)
                {
                    rigidbody.useGravity = false;
                    rigidbody.AddForce(transform.forward * forceStrength * Time.deltaTime*50-rigidbody.velocity, ForceMode.VelocityChange);
                }
                    
            }

        }
    }
}

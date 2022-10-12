using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButtonController : MonoBehaviour
{
    private bool isActive;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;

    [SerializeField] LayerMask magnetButtonMask;

    [SerializeField] float forceStrength = 1f;

    [SerializeField] MagnetButtonManager magnetButtonManager;
    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = inactiveMaterial;
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
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("MagnetBlock");
        Rigidbody[] rigidbodies = new Rigidbody[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
        {
            rigidbodies[i] = blocks[i].GetComponent<Rigidbody>();
        }
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.useGravity = true;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
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

            GameObject[] blocks = GameObject.FindGameObjectsWithTag("MagnetBlock");
            Rigidbody[] rigidbodies = new Rigidbody[blocks.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                rigidbodies[i] = blocks[i].GetComponent<Rigidbody>();
            }
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                Debug.DrawRay(blocks[0].transform.position, blocks[0].transform.position * 10, Color.blue, 20);
                // Debug.Log("Adding force " + transform.forward);
                rigidbody.useGravity = false;
                rigidbody.AddForce(transform.forward * forceStrength * Time.deltaTime, ForceMode.VelocityChange);
            }

        }
    }
}

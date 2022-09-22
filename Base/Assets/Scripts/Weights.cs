using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weights : MonoBehaviour
{
    private int total_weights;
    [SerializeField] public Transform scale;
    [SerializeField] public LayerMask boxMask;

    [SerializeField] private GameObject door;

    private DoorBehavior doorAction;
    // Start is called before the first frame update
    void Start()
    {
        total_weights = 0;
        doorAction = door.AddComponent<DoorBehavior>();
        StartCoroutine(CheckForBoxes());
    }
    
    private IEnumerator CheckForBoxes()
    {

        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            CheckBoxes();
            yield return wait;
        }
    }

    private void CheckBoxes()
    {
        Collider[] numObjects = Physics.OverlapBox(scale.position, transform.localScale, Quaternion.identity, boxMask);
        var total = numObjects.Sum(obj => int.Parse(obj.tag[..1]));
        total_weights = total;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (total_weights == 10) doorAction.changeDoor();

    }
    
    
}

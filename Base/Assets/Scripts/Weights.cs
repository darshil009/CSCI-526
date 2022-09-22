using System.Collections;
using System.Collections.Generic;
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
            CheckForPlayer();
            yield return wait;
        }
    }

    private void CheckForPlayer()
    {
        Collider[] numObjects = Physics.OverlapBox(scale.position, transform.localScale * 2, Quaternion.identity, boxMask);
        int total = 0;
        int i = 0;
        while (i < numObjects.Length)
        {
            Collider obj = numObjects[i];
            if (obj.CompareTag("1lb"))
            {
                total++;
            } else if (obj.CompareTag("2lb"))
            {
                total += 2;
            } else if (obj.CompareTag("3lb"))
            {
                total += 3;
                
            } else if (obj.CompareTag("5lb"))
            {
                total += 5;
            }

            i++;
        }
        total_weights = total;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (total_weights == 10) doorAction.changeDoor();

    }
    
    
}

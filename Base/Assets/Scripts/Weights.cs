using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Weights : MonoBehaviour
{
    private float total_weights;
    [SerializeField] public Transform scale;
    [SerializeField] public LayerMask boxMask;

    [SerializeField] private GameObject door;
    public TextMeshProUGUI weightText;

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
        Collider[] numObjects = Physics.OverlapBox(scale.position, transform.localScale/2, Quaternion.identity, boxMask);
        float total = 0;
        foreach (var obj in numObjects)
        {
            if (obj.gameObject.CompareTag("05lb"))
            {
                total += 0.5f;
            }
            else
            {
                total += int.Parse(obj.tag[..1]);
            }
            
        }
        total_weights = total;
    }

    // Update is called once per frame
    void Update()
    {
        weightText.text = total_weights + " lb";
        if (total_weights == 10.5)
        {
            doorAction.changeDoor();
            weightText.text = "Level 1 Complete";
        }
        
    }
    
    
}

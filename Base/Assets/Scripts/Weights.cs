using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Weights : MonoBehaviour
{
    public static float total_weights;
    [SerializeField] public Transform scale;
    [SerializeField] public LayerMask boxMask;

    [SerializeField] private GameObject door;
    [SerializeField] public TextMeshProUGUI weightText;
    [SerializeField] public TextMeshProUGUI doorText;

    private DoorBehavior doorAction;
    private bool isDoorOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        isDoorOpen = false;
        doorText.text = "10 lbs";
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
            if (GameDetails.weights_from_tag.ContainsKey(obj.tag))
            {
                total += GameDetails.weights_from_tag[obj.tag];
            }
        }
        total_weights = total;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDoorOpen)
            weightText.text = total_weights + "/10 lb";
        if (total_weights == 10 || isDoorOpen)
        {
            doorAction.changeDoor();
            weightText.text = "DOOR OPEN";
            doorText.text = "";
            isDoorOpen = true;
        }
        
    }
    
    
}

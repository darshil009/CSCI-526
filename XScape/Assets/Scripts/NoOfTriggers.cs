using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoOfTriggers : MonoBehaviour
{
    [SerializeField] public int Value = 1;
    public Text ValueText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ValueText.text = Value.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    private Light light;
    private bool _isEnabled;
    private LayerMask _playerMask;
    private Collider[] _results;

    // Start is called before the first frame update
    private void Start()
    {
        _results = new Collider[1];
        _playerMask = LayerMask.GetMask("Player");
        _isEnabled = false;
        light = transform.GetComponent<Light>();
        light.enabled = false;
    }

    private void Update()
    {
        
        var size = Physics.OverlapSphereNonAlloc(transform.position, 3f, _results, _playerMask);
        if (size != 0)
            _isEnabled = true;
        else
        {
            _isEnabled = false;
            light.color = Color.white;
        }
        light.enabled = _isEnabled;
    }

    private void OnMouseEnter()
    {
        if (_isEnabled)
            light.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (_isEnabled)
            light.color = Color.white;
    }
}
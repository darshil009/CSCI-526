using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPointer : MonoBehaviour
{
    public RectTransform prefab;
    private RectTransform waypoint;
    private Vector3 offset = new Vector3(0, 3.75f, 0);
    
    void Start()
    {
        var canvas = GameObject.Find("Waypoints").transform;
        waypoint = Instantiate(prefab, canvas);
    }

    void Update()
    {
        float minX = prefab.rect.width/2;
        float maxX = Screen.width - minX;

        float minY = prefab.rect.height/2;
        float maxY = Screen.height - minY;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);
        screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
        screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);

        waypoint.position = screenPos;
        waypoint.gameObject.SetActive(screenPos.z > 0);
    }
}

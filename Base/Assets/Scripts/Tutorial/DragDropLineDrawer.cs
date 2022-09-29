using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropLineDrawer : MonoBehaviour
{
    // Start is called before the first frame update
     private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(LineDraw());
    }

        IEnumerator LineDraw()
    {
        float t = 0;
        float time = 2;
        Vector3 orig = lineRenderer.GetPosition(0);
        Vector3 orig2 = lineRenderer.GetPosition(1);
        lineRenderer.SetPosition(1, orig);
        Vector3 newpos;
        for (; t < time; t += Time.deltaTime)
        {
            newpos = Vector3.Lerp(orig, orig2, t / time);
            lineRenderer.SetPosition(1, newpos);
            yield return null;
        }
        lineRenderer.SetPosition(1, orig2);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyIcon : MonoBehaviour
{
    public Image enemyPrefab;
    private Image uiUse;

    // public Image healthEnemyPrefab;
    // private Image uiUseHealth;

    private Transform tr_head;
    private Vector3 offset = new Vector3(0,1.5f,0);
    // Start is called before the first frame update
    void Start()
    {
        uiUse = Instantiate(enemyPrefab,FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        //uiUseHealth = Instantiate(healthEnemyPrefab,FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        tr_head = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        uiUse.transform.position = Camera.main.WorldToScreenPoint(tr_head.position+offset);
        //uiUseHealth.transform.position = Camera.main.WorldToScreenPoint(tr_head.position+offset);
    }
}

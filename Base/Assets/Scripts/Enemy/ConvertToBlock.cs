using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToBlock : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject weightFromEnemy;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript!=null)
        {
            Destroy(transform.parent.gameObject);
            weightFromEnemy.SetActive(true);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour
{
    private float SPEED = 400f;
    public GameObject spikeBall;
    private GameObject[] gemsWithEnemies;
    private GameObject[] spikeBalls;
    // Start is called before the first frame update
    void Start()
    {
        gemsWithEnemies = GameObject.FindGameObjectsWithTag("GEM_WITH_ENEMY");
        spikeBalls = new GameObject[gemsWithEnemies.Length];
        for(int i = 0; i<gemsWithEnemies.Length; i++)
        {
            Vector3 spikeBallPos = new Vector3(gemsWithEnemies[i].transform.position.x, 
                                            gemsWithEnemies[i].transform.position.y + 0.3f, 
                                            gemsWithEnemies[i].transform.position.z + 1.5f);
            GameObject spikeBallObj = Instantiate(spikeBall, spikeBallPos, Quaternion.identity);
            spikeBalls[i] = spikeBallObj;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<gemsWithEnemies.Length; i++)
        {
            GameObject gem = gemsWithEnemies[i];
            GameObject spikeBallObj = spikeBalls[i];
            //print("gem pos: " + gem.transform.position);
            //print("spike: "  + spikeBallObj.transform.position);
            spikeBallObj.transform.RotateAround(gem.transform.position, Vector3.up, SPEED * Time.deltaTime);
        }
    }
}

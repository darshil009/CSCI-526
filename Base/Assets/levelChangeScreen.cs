using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelChangeScreen : MonoBehaviour
{
    public LevelCompleteScript levelCompleteScript = new LevelCompleteScript();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            Time.timeScale = 0;
            levelCompleteScript.levelCompleteDisplay();

        }
    }

}

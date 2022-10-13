using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //event to notify other objects when game is won.
    //restartbutton/restartscreen should subscribe to this.

    [SerializeField] private UnityEvent gameWonEvent;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
}

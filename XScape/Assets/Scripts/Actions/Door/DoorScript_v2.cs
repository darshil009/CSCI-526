using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript_v2 : MonoBehaviour
{

    [SerializeField] private List<TriggerScript> triggerList;

    private Transform slidingDoor;
    private Transform frame;

    private int activeTriggersCount = 0;
    private bool isDoorOpen = false;

    Vector3 closePosition;

    private string SLIDING_DOOR = "Gate_Small/Sliding_Door";
    private string FRAME = "Gate_Small/Frame";
    private float SLIDE_DISTANCE_Z = 1.26f;
    public float DURATION = 1;

    private void OnEnable(){
        foreach(TriggerScript triggerScript in triggerList){
            triggerScript.triggerActiveSub += addActiveTriggers;
            triggerScript.triggerInActiveSub += subtractActiveTriggers;
        }
        
    }

    private void OnDisable() {

        foreach(TriggerScript triggerScript in triggerList){
            triggerScript.triggerActiveSub -= addActiveTriggers;
            triggerScript.triggerInActiveSub -= subtractActiveTriggers;
        }
    }
     
    // Start is called before the first frame update
    void Start()
    {
        activeTriggersCount = 0;
        slidingDoor = this.transform.Find(SLIDING_DOOR);
        frame = this.transform.Find(FRAME);
        closePosition = slidingDoor.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        //Player completed level
        if(other.CompareTag("Player")){
            Debug.Log("DOOR V2: Level Complete");

            //TODO : cursor control shouldnt be in door, change this later
            Cursor.lockState = CursorLockMode.None;

            SceneManager.LoadScene("LevelComplete", LoadSceneMode.Additive);
        }
    }

    
    public void addActiveTriggers(){
        activeTriggersCount += 1;
        if(activeTriggersCount == triggerList.Count){
            OperateDoor();
        }

    }
    
    public void subtractActiveTriggers(){
        activeTriggersCount += -1;
        if(activeTriggersCount == triggerList.Count-1){
            OperateDoor();
        }

    }
    
    void OperateDoor(){
        StopAllCoroutines();
        if (!isDoorOpen)
        {
            Vector3 openPosition = closePosition + Vector3.forward * SLIDE_DISTANCE_Z;
            StartCoroutine(MoveDoor(openPosition));
        }
        else
        {
            StartCoroutine(MoveDoor(closePosition));
        }
        isDoorOpen = !isDoorOpen;
    }

    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float timeElapsed = 0;
        Vector3 startPosition = slidingDoor.localPosition;
        while (timeElapsed < DURATION)
        {
            slidingDoor.localPosition = Vector3.Lerp(startPosition, targetPosition, timeElapsed / DURATION);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        slidingDoor.localPosition = targetPosition;
    }
}

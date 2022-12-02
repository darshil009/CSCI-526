using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript_v2 : MonoBehaviour
{

    [SerializeField] private List<TriggerScript> triggerList;

    private Transform slidingDoor;
    private Transform frame;
    private SentToGoogle sg = new SentToGoogle();
    private int activeTriggersCount = 0;
    private bool isDoorOpen = false;

    Vector3 closePosition;

    private string SLIDING_DOOR = "Gate_Small/Sliding_Door";
    private string FRAME = "Gate_Small/Frame";
    private float SLIDE_DISTANCE_Z = 1.26f;
    public float DURATION = 1;
    AudioSource audioData;
    private float startTime;
    private bool isLevelComplete;

    // private string levelSessionId = System.Guid.NewGuid().ToString();
    private int numActivatedTriggers=0;
    private void OnEnable(){
        if(!TriggerActivations.levelName.Equals("DUMMY"))
        {
            StartCoroutine(sg.Post5(TriggerActivations.levelName,TriggerActivations.triggerActiveCount.ToString(),"INCOMPLETE"));
        }
        TriggerActivations.reset();
        TriggerActivations.levelName =  SceneManager.GetActiveScene().name;

        foreach(TriggerScript triggerScript in triggerList){
            triggerScript.triggerActiveSub += addActiveTriggers;
            triggerScript.triggerInActiveSub += subtractActiveTriggers;
        }
        
    }


    private void OnDisable() {
        Debug.Log("Disable "+triggerList.Count+ " "+activeTriggersCount);
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
        startTime = Time.time;
        isLevelComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        //Player completed level
        if(other.CompareTag("Player") && !isLevelComplete){
            isLevelComplete = true;
            int magnetClick1 = MagnetButtonController.magnetClick;
            int platformClick1 = platformMove.movingPlatformClick;
            string levelName = StarterAssets.FirstPersonController.sceneName;
            StartCoroutine(sg.Post1(levelName, magnetClick1, platformClick1));
            StartCoroutine(sg.Post2(levelName, "Completed"));
            string sessionid = StarterAssets.FirstPersonController.sess_id;
            StartCoroutine(sg.Post3(sessionid, levelName));
            StartCoroutine(sg.Post4(levelName,(Time.time-startTime).ToString()));
            Debug.Log("DOOR V2: Level Complete");
            Debug.Log("levelName:");
            Debug.Log(levelName);
            //TODO : cursor control shouldnt be in door, change this later

            StartCoroutine(sg.Post5(TriggerActivations.levelName,TriggerActivations.triggerActiveCount.ToString(),"COMPLETE"));
            TriggerActivations.reset();

            Cursor.lockState = CursorLockMode.None;
            if (string.Equals(levelName, "L25"))
            {
                SceneManager.LoadScene("LastLevelComplete", LoadSceneMode.Additive);
            }
            else{
                SceneManager.LoadScene("LevelComplete", LoadSceneMode.Additive);
            }
            
        }
    }

    
    public void addActiveTriggers(){
        TriggerActivations.triggerActiveCount++;
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
            audioData = GetComponent<AudioSource>();
            Vector3 openPosition = closePosition + Vector3.forward * SLIDE_DISTANCE_Z;
            StartCoroutine(MoveDoor(openPosition));
            audioData.Play(0);
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

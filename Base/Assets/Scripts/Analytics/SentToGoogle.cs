using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class SentToGoogle
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc_RXFRLw-bVQ4KFvU2pWO8XjQl1chBg5PCFLhoaeRDrs0W1A/formResponse";
    


    public void Send(string level, float timeLost, float healthLost)
    {
        // Assign variables
        //level1 = level;
        //_testBool = true;
        //_testFloat = Random.Range(0.0f, 10.0f);
        Debug.Log("checking........." + " level: " + level + " timeLost: " + timeLost + " health: " + healthLost);
        
    }

    public IEnumerator Post(string level, string timeLost, string healthLost)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        // form.AddField("entry.1043481993", level);
        // form.AddField("entry.1258024626", timeLost);
        // form.AddField("entry.1136302750", healthLost);
        //form.AddField("entry.2106587218", level);
        //form.AddField("entry.1708535033", timeLost);
        //form.AddField("entry.372281974", healthLost);
        form.AddField("entry.394529085", level);
        form.AddField("entry.643418424", timeLost);
        form.AddField("entry.842546645", healthLost);
        
            
            
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

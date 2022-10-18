using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class SentToGoogle
{

    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdY8JPBtGPA9guKREuXZaTb-93qWYP2EhsIcu2RlFUzkaPIfg/formResponse";

    // public void Send(string level, float timeLost, float healthLost)
    // {
    //     // Debug.Log("checking........." + " level: " + level + " timeLost: " + timeLost + " health: " + healthLost);
    // }

    public IEnumerator Post() // string level, string session
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();

        // form.AddField("entry.812308484", session);
        // form.AddField("entry.2075391151", levelcompleted.ToString());

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

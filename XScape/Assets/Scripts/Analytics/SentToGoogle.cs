using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class SentToGoogle
{

    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfwpNdCMG8FjTRod6hXN9ujf7AAxcsTLO4xdO93AXabnbC86A/formResponse";
    [SerializeField] private string URL1 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScC-ItNJBIiz8qOD-bbL0dWcqIGQa_6YW0Fnz84anX2L1xNMg/formResponse";

    // public void Send(string level, float timeLost, float healthLost)
    // {
    //     // Debug.Log("checking........." + " level: " + level + " timeLost: " + timeLost + " health: " + healthLost);
    // }

    public IEnumerator Post(string level, string noOfRestarts) // string level
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        WWWForm form1 = new WWWForm();
        Debug.Log("In post method, restarts:"+noOfRestarts);
        // form.AddField("entry.812308484", session);
        // form.AddField("entry.2075391151", levelcompleted.ToString());
        // form.AddField("entry.1697405424", totalTimeTaken);
        // form.AddField("entry.565529449", levelsStarted);
        form.AddField("entry.760709253", level);
        form.AddField("entry.1251335535", noOfRestarts);
        Debug.Log("Added to form");
        // form.AddField("entry.710034409", noOfMagnetClicks);
        // form.AddField("entry.1842277599", rating);
        // form.AddField("entry.1410116048", sessionId);
        // form1.AddField("entry.897732532",level);

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

    public IEnumerator Post1(string level, string totalTimeTaken) // string level
    {
        // Create the form and enter responses
        WWWForm form1 = new WWWForm();
        // form.AddField("entry.812308484", session);
        // form.AddField("entry.2075391151", levelcompleted.ToString());
        // form.AddField("entry.1697405424", totalTimeTaken);
        // form.AddField("entry.565529449", levelsStarted);
        form1.AddField("entry.897732532", level);
        
        // form.AddField("entry.710034409", noOfMagnetClicks);
        // form.AddField("entry.1842277599", rating);
        // form.AddField("entry.1410116048", sessionId);
        // form1.AddField("entry.897732532",level);
        form1.AddField("entry.1380655407",totalTimeTaken);
        Debug.Log("Added to form");

        

        using (UnityWebRequest www = UnityWebRequest.Post(URL1, form1))
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

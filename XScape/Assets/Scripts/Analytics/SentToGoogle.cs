using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class SentToGoogle
{

    [SerializeField] private string URL1 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSedKF0pgUx4-PPkuv9LMHY7NMwwVqBCHmlaV3vcU33r7MMUUA/formResponse";
    [SerializeField] private string URL2 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdSnww8TEMchcQ7LRontDMiwtbFSpzy7TjvKjBQTSVyzigBmQ/formResponse";

    // public void Send(string level, float timeLost, float healthLost)
    // {
    //     // Debug.Log("checking........." + " level: " + level + " timeLost: " + timeLost + " health: " + healthLost);
    // }

    //public IEnumerator Post() // string level, string session
    //{
    //    // Create the form and enter responses
    //    WWWForm form = new WWWForm();

    //    // form.AddField("entry.812308484", session);
    //    // form.AddField("entry.2075391151", levelcompleted.ToString());

    //    using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
    //    {
    //        yield return www.SendWebRequest();
    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Form upload complete!");
    //        }
    //    }

    //}

    public IEnumerator Post1(string level, int numberClick, int platformClick) // string level, string session
    {
        Debug.Log("Inside Post1" + level + ", " + numberClick);
        // Create the form and enter responses
        //LevelComplete lc = new LevelComplete();
        //WWWForm form = lc.form;

        WWWForm form = new WWWForm();
        form.AddField("entry.1260238080", level);
        form.AddField("entry.1645627813", numberClick.ToString());
        form.AddField("entry.1444043565", platformClick.ToString());


        using (UnityWebRequest www = UnityWebRequest.Post(URL1, form))
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

    public IEnumerator Post2(string level, string status) // string level, string session
    {
        Debug.Log("Inside Post2" + level);
        // Create the form and enter responses
        //LevelComplete lc = new LevelComplete();
        //WWWForm form = lc.form;

        WWWForm form = new WWWForm();
        form.AddField("entry.1025182207", level);
        form.AddField("entry.1416845053", status);


        using (UnityWebRequest www = UnityWebRequest.Post(URL2, form))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class SentToGoogle
{

    [SerializeField] private string URL1 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSedKF0pgUx4-PPkuv9LMHY7NMwwVqBCHmlaV3vcU33r7MMUUA/formResponse";
    [SerializeField] private string URL2 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdSnww8TEMchcQ7LRontDMiwtbFSpzy7TjvKjBQTSVyzigBmQ/formResponse";
    [SerializeField] private string URL3 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfLi1RAnN2vBx2Mytxr3-fujXBxk4WwX84F5UlBWxNTvSI8Fg/formResponse";
    [SerializeField] private string URL4 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScC-ItNJBIiz8qOD-bbL0dWcqIGQa_6YW0Fnz84anX2L1xNMg/formResponse";

    [SerializeField] private string URL5 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeNoPfA6mWLprE7U-xuUCevBubR5ilD00b9sAiNoEM9wt0x2w/formResponse";

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

    public IEnumerator Post3(string sesionId, string level) // string level, string session
    {
        Debug.Log("Inside Post2" + level);
        // Create the form and enter responses
        //LevelComplete lc = new LevelComplete();
        //WWWForm form = lc.form;

        WWWForm form = new WWWForm();
        form.AddField("entry.233370001", sesionId);
        form.AddField("entry.86418165", level);


        using (UnityWebRequest www = UnityWebRequest.Post(URL3, form))
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


    public IEnumerator Post4(string level, string totalTimeTaken) // string level, string total time taken
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.897732532", level);
        form.AddField("entry.1380655407",totalTimeTaken);


        using (UnityWebRequest www = UnityWebRequest.Post(URL3, form))
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


        public IEnumerator Post5(string levelName, string numActivatedTriggers,string data) // string level, string total time taken
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.897732532", levelName);
        form.AddField("entry.1380655407",numActivatedTriggers);
        form.AddField("entry.1526511764",data);

        using (UnityWebRequest www = UnityWebRequest.Post(URL5, form))
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

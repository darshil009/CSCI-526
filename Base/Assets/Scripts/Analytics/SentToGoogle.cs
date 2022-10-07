using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class SentToGoogle
{
    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfTiFRIZN6NmPcWoNikLwz8o5-Hc4xAOIJNvbDT3VS1J2H3mA/formResponse";
    [SerializeField] private string URL1 = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfvlEjFevN39EYAyKAxE7leS-AhjJ2b-mXFzNqzJD4exvJ2Uw/formResponse";
    public static List<string> entryListTime = new List<string>();
    public static List<string> entryListHealth = new List<string>();
    public static List<string> entryListWeights = new List<string>();

    public void Send(string level, float timeLost, float healthLost)
    {
        // Assign variables
        //level1 = level;
        //_testBool = true;
        //_testFloat = Random.Range(0.0f, 10.0f);
        Debug.Log("checking........." + " level: " + level + " timeLost: " + timeLost + " health: " + healthLost);
        
    }

    public IEnumerator Post(string level, List<int> timeList, List<int> healthList, List<int> weightList, string completed, string funct, string weights_remaining, string time_remaining, int died, string session)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        WWWForm form1 = new WWWForm();
        Debug.Log("How player died:" + died);
        entryListTime.Add("entry.1510320122");
        entryListTime.Add("entry.1043481993");
        entryListTime.Add("entry.1258024626");
        entryListTime.Add("entry.1136302750");
        entryListTime.Add("entry.1328267169");
        entryListTime.Add("entry.155225513");
        entryListTime.Add("entry.1278632406");


        entryListHealth.Add("entry.363407111");
        entryListHealth.Add("entry.1077862492");
        entryListHealth.Add("entry.1215918979");
        entryListHealth.Add("entry.11196490");
        entryListHealth.Add("entry.1018968405");
        entryListHealth.Add("entry.1397124904");
        entryListHealth.Add("entry.1136446190");


        entryListWeights.Add("entry.1417587008");
        entryListWeights.Add("entry.27760501");
        entryListWeights.Add("entry.957185104");
        entryListWeights.Add("entry.642803933");
        entryListWeights.Add("entry.942943561");
        entryListWeights.Add("entry.1901821866");
        entryListWeights.Add("entry.1377211215");

        // form.AddField("entry.1043481993", level);
        // form.AddField("entry.1258024626", timeLost);
        // form.AddField("entry.1136302750", healthLost);
        //form.AddField("entry.2106587218", level);
        //form.AddField("entry.1708535033", timeLost);
        //form.AddField("entry.372281974", healthLost);
        //form.AddField("entry.394529085", level);
        //form.AddField("entry.643418424", timeLost);
        //form.AddField("entry.842546645", healthLost);
        //form.AddField("entry.1636130689", total_weights);


        //entry.1823601596 : Time 30
        //entry.1149332147 : Time: 60
        //entry.1832287611 : Time 90
        //entry.616110772 : Time 120
        //entry.880179314 : Time 150
        //entry.1903063886 : Time 180
        //entry.2081236379 : health 30
        //entry.1508888616 : health 60
        //entry.1961570879 : health 90
        //entry.2119062654 : health 120
        //entry.308061001 : health 150
        //entry.150889121 : health 180
        //entry.818728102 : weights 30
        //entry.1785092299 : weights 60
        //entry.987648654 : weights 90
        //entry.1735320421 : weights 120
        //entry.2063289579 : weights 150
        //entry.709955127 : weights 180
        //entry.711737386 : weights 0
        //entry.1901563906 : Time 0
        //entry.210257491 : health 0
        //entry.680257617 : level
        //entry.1661973971 : completed
        //entry.2038632042 : time remaining
        //entry.1978893941 : weights remaing

        for (int i = 0; i < timeList.Count; i++)
        {
            form.AddField(entryListTime[i], timeList[i].ToString());
            form.AddField(entryListHealth[i], healthList[i].ToString());
            form.AddField(entryListWeights[i], weightList[i].ToString());
        }
        Debug.Log("There.................");
        // form.AddField("entry.680257617", level);
        form.AddField("entry.370164813", level);
        // form.AddField("entry.1661973971", completed);
        form.AddField("entry.188070378", completed);
        Debug.Log("There1.................");
        if (time_remaining != null)
        {
            // form.AddField("entry.2038632042", time_remaining);
            form.AddField("entry.390073540", time_remaining);
        }
        Debug.Log("There2.................");
        if (weights_remaining != null)
        {
            // form.AddField("entry.1978893941", weights_remaining);
            form.AddField("entry.1954548142", weights_remaining);
        }
        // form.AddField("entry.1043481993", level);
        // form.AddField("entry.1258024626", timeLost);
        // form.AddField("entry.1136302750", healthLost);
        //form.AddField("entry.2106587218", level);
        //form.AddField("entry.1708535033", timeLost);
        //form.AddField("entry.372281974", healthLost);
        //form.AddField("entry.394529085", level);
        //form.AddField("entry.643418424", timeLost);
        //form.AddField("entry.842546645", healthLost);
        //form.AddField("entry.1636130689", total_weights);
        Debug.Log("==================**********=============================");
        Debug.Log("funct =" + funct);
        Debug.Log("Level =" + level);
        Debug.Log("completed =" + completed);

        Debug.Log("weights_remaining =" + weights_remaining);


        Debug.Log("time_remaining =" + time_remaining);

        for (int i = 0; i < timeList.Count; i++)
        {
            Debug.Log("timeList =" + timeList[i]);
        }

        for (int i = 0; i < healthList.Count; i++)
        {
            Debug.Log("healthList =" + healthList[i]);
        }

        for (int i = 0; i < weightList.Count; i++)
        {
            Debug.Log("weightList =" + weightList[i]);
        }

        
        

        form.AddField("entry.1031659999", session);
        if (died != 4)
        {
            form1.AddField("entry.2023968175", died.ToString());
        }
        form.AddField("entry.2075391151", level.ToString());
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

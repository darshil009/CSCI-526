using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager
{
    int level;
    private static AnalyticsManager instance;
    // Restart res;

    // public void Reset(int curLevel)
    // {
    //     level = curLevel;
        
    // }

    public void RegisterEvent(GameEvent gameEvent, object data)
    {

        Debug.Log("Event registered");
        switch (gameEvent)
        {
            // case GameEvent.<PLACEHOLDER>:
            //     {
            //         
            //         break;
            //     }
            
            default: break;
        }

    }

    public IDictionary<string, string> Publish()
    {
        Debug.Log("Publishing");
        IDictionary<string, string> analytics = new Dictionary<string, string>();
        // analytics.Add("restarts", restart_count.ToString());
        // analytics.Add("level", level.ToString());
        // analytics.Add("level", level.ToString());
        
        

        return analytics;
    }
}

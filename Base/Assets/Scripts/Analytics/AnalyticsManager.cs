using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager
{
    int level;
    float timeLost;
    float healthLost;
    

    private static AnalyticsManager instance;
    // public static AnalyticsManager GetAnalyticsManager()
    // {
    //     if (instance == null)
    //     {
    //         instance = new AnalyticsManager();
    //         instance.Reset("0");
    //     }
    //     return instance;
    // }

    public void Reset(int curLevel)
    {
        level = curLevel;
        timeLost = 0;
        healthLost = 0;
    }

    public void RegisterEvent(GameEvent gameEvent, object data)
    {

        Debug.Log("Event registered");
        switch (gameEvent)
        {
            case GameEvent.TIME_UP:
                {
                    timeLost++;
                    break;
                }
            case GameEvent.HEALTH_LOST:
                {
                    healthLost++;
                    break;
                }
            default: break;
        }

    }

    public IDictionary<string, string> Publish()
    {
        Debug.Log("Publishing");
        IDictionary<string, string> analytics = new Dictionary<string, string>();
        analytics.Add("level", level.ToString());
        analytics.Add("time", timeLost.ToString());
        analytics.Add("health", healthLost.ToString());
        // Debug.Log("Analytics Results:" + analytics["level"]);
        // Debug.Log("Analytics Time Results:" + analytics["Time remaining"]);
        // Debug.Log("Analytics Health Results:" + analytics["Health remianing"]);

        return analytics;
        // 
        // SentToGoogle sg = sgGameObject.AddComponent<SentToGoogle>();
        // sg.Send(level, timeLost, healthLost);
        //analytics.Add("totalCoins", totalCoins);

        //analytics.Add("timeToReachTarget", timeToReachTarget);
        //analytics.Add("remainingTime", remainingTime);
        //analytics.Add("totalTime", totalTime);

        //analytics.Add("pointsAtDeath", pointsAtDeath);
        //analytics.Add("exitReason", exitReason);

        //AnalyticsResult analyticsResult = Analytics.CustomEvent("userData", analytics);
        //if (analyticsResult != AnalyticsResult.Ok)
        //{
        //    Debug.LogError(analytics);
        //    Debug.LogError("Something went wrong while publishing");
        //}
        //analyticsResult = Analytics.CustomEvent("powerUpUsed", powerUpUsed);
        //if (analyticsResult != AnalyticsResult.Ok)
        //{
        //    Debug.LogError(analytics);
        //    Debug.LogError("Something went wrong while publishing");
        //}

        //analyticsResult = Analytics.CustomEvent("collidedObstacles", collidedObstacles);
        //if (analyticsResult != AnalyticsResult.Ok)
        //{
        //    Debug.LogError(analytics);
        //    Debug.LogError("Something went wrong while publishing");
        //}
    }


}

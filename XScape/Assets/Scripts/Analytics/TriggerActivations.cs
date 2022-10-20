using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class TriggerActivations 
{
    public static int triggerActiveCount=0;
    public  static string levelComplete="INCOMPLETE";
    
    public static string levelName="DUMMY";
    public static void reset()
    {
        triggerActiveCount = 0;
        levelComplete = "INCOMPLETE";
        levelName ="DUMMY";
    }
}
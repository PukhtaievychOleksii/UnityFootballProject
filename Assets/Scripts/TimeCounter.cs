using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter 
{
   
    private float StartCountingTime = 0;
    public void  SetToZeroCounter()
    {
        StartCountingTime = Time.time;   
    }

    public float  GetPassedTime()
    {
        return Time.time - StartCountingTime;
    }
}

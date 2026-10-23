using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float maxTime;
    float timeOfStart;
    public float GetTime() {
        return Mathf.Min(maxTime, Time.time - timeOfStart);
    }

    public void restartTimer() {
        timeOfStart = Time.time;        
    }

    public bool completed() {
        return timeOfStart + maxTime < Time.time;
    }

    public Timer(float _maxTime) { 
        maxTime = _maxTime;
        timeOfStart = -maxTime;
    }

    public static Timer Longer(Timer a, Timer b) { 
        return a.timeOfStart + a.maxTime > b.timeOfStart + b.maxTime ? a : b;    
    }

    public static Timer Shorter(Timer a, Timer b) { 
        return a.timeOfStart + a.maxTime < b.timeOfStart + b.maxTime ? a : b;    
    }

}

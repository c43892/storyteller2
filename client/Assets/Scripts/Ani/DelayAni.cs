using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAni : IAni
{
    public string Name { get; set; }
    public bool Started { get; set; }

    float timeLeft;
    public bool Ended 
    {
        get 
        { 
            if (timeLeft <= 0)
                return true;
            return false;
        } 
    }

    public void Start() 
    {
        Started = true; 
    }

    public void Update(float timeElapsed)
    {
        if (!Started)
            throw new Exception("Not started!!!");
        timeLeft -= timeElapsed;
    }

    public DelayAni(float sec)
    {
        timeLeft = sec;
    }
}

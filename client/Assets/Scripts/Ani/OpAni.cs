using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpAni : IAni
{
    Action act;

    public OpAni(Action action)
    {
        act = action;
    }

    public string Name { get; set; }
    public bool Started { get; set; }
    public bool Ended { get; set; }

    public void Start() 
    { 
        Started = true;
        Ended = false;
    }

    public void Update(float timeElapsed)
    {
        if (!Started)
            throw new Exception("Not started!!!");
        act();
        Ended = true;
    }
}

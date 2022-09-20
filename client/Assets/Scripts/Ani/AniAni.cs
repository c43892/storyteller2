using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniAni : MonoBehaviour
{
    Action act;

    public AniAni(Action action)
    {
        act = action;
    }

    public string Name { get; set; }
    public bool Started { get; set; }
    public bool Ended { get; set; }

    void Start()
    {
        Started = true;
        Ended = false;
    }

    void Update()
    {
        if (!Started)
            throw new Exception("Not started!!!");
        act();
        Ended = true;
    }
}

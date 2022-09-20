using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForClick : IAni
{
    public string Name { get; set; }
    public bool Started { get; set; }
    public bool Ended { get; set; }
    public void Start()
    {
        Started = true;
    }
    public void Update(float tiemElapsed)
    {
        Ended = true;
    }
}

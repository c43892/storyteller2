using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAni
{
    string Name { get; }
    bool Started { get; }
    bool Ended { get; }
    void Start();
    void Update(float tiemElapsed);
}

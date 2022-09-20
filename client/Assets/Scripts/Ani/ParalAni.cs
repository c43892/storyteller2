using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalAni : IAni
{
    IAni[] arr;
    
    public ParalAni(params IAni[] aniArr)
    {
        arr = aniArr;
    }

    public string Name { get; set; }

    public bool Started
    {
        get
        {
            foreach (var ani in arr)
                if (!ani.Started)
                    return false;
            return true;
        }
    }

    public bool Ended
    {
        get
        {
            foreach (var ani in arr)
                if (!ani.Ended)
                    return false;
            return true;
        }
    }

    public void Start()
    {
        foreach (var ani in arr)
            ani.Start();
    }

    public void Update(float timeElapsed)
    {
        foreach (var ani in arr)
            ani.Update(timeElapsed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqAni : IAni
{
    IAni[] arr;
    
    public SeqAni (params IAni[] aniArr)
    {
        arr = aniArr;
    }

    public string Name { get; set; }
    public bool Started { get; set; }

    public bool Ended
    {
        get
        {
            if (!arr[arr.Length - 1].Ended)
                return false;

            return true;
        }
    }

    int activeAni = 0;

    public void Start()
    {
        Started = true;
        arr[activeAni].Start();
    }

    public void Update(float timeElapsed)
    {
        if (activeAni >= arr.Length)
            return;

        if (arr[activeAni].Ended)
        {
            activeAni++;
            if (activeAni < arr.Length)
                arr[activeAni].Start();
        }

        if (activeAni < arr.Length)
            arr[activeAni].Update(timeElapsed);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniPlayer
{
    IAni curAni;

    /*void Play(IAni ani)
    {
        curAni = ani;
        curAni.Start();
    }*/

    public IEnumerator Play(IAni ani)
    {
        curAni = ani;
        curAni.Start();
        while (!curAni.Ended)
            yield return null;
    }

    public void Update(float timeElapsed)
    {
        curAni.Update(timeElapsed);
    }
}

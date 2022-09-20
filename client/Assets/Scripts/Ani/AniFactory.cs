using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniFactory
{
    public IEnumerator Play(IAni ani)
    {
        ani.Start();
        while (!ani.Ended)
            yield return null;
    }
}

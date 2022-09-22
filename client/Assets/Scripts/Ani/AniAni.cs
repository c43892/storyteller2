using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniAni : IAni
{
    Animator anim;

    public AniAni(Animator animator)
    {
        anim = animator;
    }

    public AniAni(BaseObjects go)
        : this(go.GetComponentInChildren<Animator>()) { }

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

        anim.Update(Time.deltaTime);
        Ended = true;
    }
}

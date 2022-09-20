using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Page Page1;
    public AniPlayer aniPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Page1.Objects.ToList().ForEach(obj =>
        {
            obj.Shown = true;
            obj.FullUncolored = true;
        });

        var dryer = Page1.GetObj("dryer");
        (new List<string> { "door1", "door2", "fan2" }).ForEach(objName => dryer.Get(objName).Shown = false);

        var washer = Page1.GetObj("washer");
        (new List<string> { "door1", "door2", "swirl" }).ForEach(objName => washer.Get(objName).Shown = false);

        Page1.ResetAllZ(0);
        //StartCoroutine(RunProcess(P1));
        var op1 = new OpAni(() => {
            washer.Get("door1").Shown = true;
            washer.Get("door2").Shown = false;
        });

        aniPlayer = new AniPlayer();
        StartCoroutine(aniPlayer.Play(new SeqAni(new DelayAni(1), new OpAni(() => washer.FullColored = true), new DelayAni(1), op1)));
    }

    private void Update()
    {
        aniPlayer.Update(Time.deltaTime);
    }

    IEnumerator RunProcess(Func<Action[]> processGenerator)
    {
        foreach (var step in processGenerator())
        {
            if (step != null) step.Invoke();
            yield return new WaitForSeconds(1);
        }
    }

    Action[] P1()
    {
        var dryer = Page1.GetObj("dryer");
        var washer = Page1.GetObj("washer");
        var swirl = washer.Get("swirl");

        return new Action[]
        {
            null,

            // washer
            () => washer.FullColored = true,
            () => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; },
            () => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = true; },
            () => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; },
            () => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = false; },

            () => { swirl.Shown = true; swirl.GetAll("1", "2", "3").ToList().ForEach(obj => obj.Shown = false); },
            () => { swirl.Get("1").Shown = false; swirl.Get("2").Shown = true; swirl.Get("3").Shown = false;},
            () => { swirl.Get("1").Shown = false; swirl.Get("2").Shown = false; swirl.Get("3").Shown = true;},
            () => { swirl.Get("1").Shown = false; swirl.Get("2").Shown = true; swirl.Get("3").Shown = false;},
            () => { swirl.Get("1").Shown = true; swirl.Get("2").Shown = false; swirl.Get("3").Shown = false;},
            () => swirl.Shown = false,

            () => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; },
            () => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = true; },
            () => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; },
            () => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = false; },

            // dryer
            () => dryer.FullColored = true,
            () => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; },
            () => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = true; },
            () => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; },
            () => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = false; },

            () => { dryer.Get("fan1").Shown = false; dryer.Get("fan2").Shown = true; },
            () => { dryer.Get("fan1").Shown = true; dryer.Get("fan2").Shown = false; },

            () => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; },
            () => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = true; },
            () => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; },
            () => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = false; },

            // end
            () => { washer.FullUncolored = true; dryer.FullUncolored = true; },
        };
    }
}

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
        /*
         * Page1.Objects.ToList().ForEach(obj =>
        {
            obj.Shown = true;
            obj.FullUncolored = true;
        });

        var dryer = Page1.GetObj("dryer");
        (new List<string> { "door1", "door2", "fan2" }).ForEach(objName => dryer.Get(objName).Shown = false);

        var washer = Page1.GetObj("washer");
        (new List<string> { "door1", "door2", "swirl" }).ForEach(objName => washer.Get(objName).Shown = false);

        Page1.ResetAllZ(0);

        StartCoroutine(RunProcess(P1));
        */
        
        aniPlayer = new AniPlayer();
        var ani = MakeAni();
        StartCoroutine(aniPlayer.Play(ani));
    }

    private void Update()
    {
        aniPlayer.Update(Time.deltaTime);
    }

    IAni MakeAni()
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

        var swirl = washer.Get("swirl");

        var mom = Page1.GetObj("mom");
        (new List<string> { "head", "body", "left hand", "right hand", "skirt", "left leg", "right leg" }).ForEach(objName => mom.Get(objName).Shown = false);

        var notes = Page1.GetObj("notes");
        (new List<string> { "note1", "note2", "note3" }).ForEach(objName => notes.Get(objName).Shown = false);

        var basket = Page1.GetObj("basket");
        (new List<string> { "empty basket", "basket one", "basket two", "basket three" }).ForEach(objName => basket.Get(objName).Shown = false);

        Page1.ResetAllZ(0);

        ParalAni paralAni;
        SeqAni seqAni;
        AniAni noteAni;
        AniAni momAni;

        seqAni = new SeqAni(
            new DelayAni(1),
            new OpAni(() => washer.FullColored = true),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = true; }),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = false; }),
            new DelayAni(1),

            new OpAni(() => { swirl.Shown = true; swirl.GetAll("1", "2", "3").ToList().ForEach(obj => obj.Shown = false); }),
            new DelayAni(1),
            new OpAni(() => { swirl.Get("1").Shown = false; swirl.Get("2").Shown = true; swirl.Get("3").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { swirl.Get("1").Shown = false; swirl.Get("2").Shown = false; swirl.Get("3").Shown = true; }),
            new DelayAni(1),
            new OpAni(() => { swirl.Get("1").Shown = false; swirl.Get("2").Shown = true; swirl.Get("3").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { swirl.Get("1").Shown = true; swirl.Get("2").Shown = false; swirl.Get("3").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => swirl.Shown = false),
            new DelayAni(1),

            new OpAni(() => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = true; }),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = true; washer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { washer.Get("door1").Shown = false; washer.Get("door2").Shown = false; }),
            new DelayAni(1),

            new OpAni(() => dryer.FullColored = true),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = true; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = false; }),
            new DelayAni(1),

            new OpAni(() => { dryer.Get("fan1").Shown = false; dryer.Get("fan2").Shown = true; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("fan1").Shown = true; dryer.Get("fan2").Shown = false; }),
            new DelayAni(1),

            new OpAni(() => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = true; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = true; dryer.Get("door2").Shown = false; }),
            new DelayAni(1),
            new OpAni(() => { dryer.Get("door1").Shown = false; dryer.Get("door2").Shown = false; }),
            new DelayAni(1),

            new OpAni(() => { washer.FullUncolored = true; dryer.FullUncolored = true; })
        );

        noteAni = new AniAni(notes.Get("note1").ColoredGo.GetComponent<Animator>());
        momAni = new AniAni(mom.Get("head").ColoredGo.GetComponent<Animator>());

        paralAni = new ParalAni(
            new OpAni(() => { mom.FullColored = true; mom.Get("head").Shown = true; mom.Get("body").Shown = true; mom.Get("left hand").Shown = true; mom.Get("right hand").Shown = true; mom.Get("left leg").Shown = true; mom.Get("right leg").Shown = true;
                mom.Get("skirt").Shown = true; notes.FullColored = true; notes.Get("note1").Shown = true; notes.Get("note2").Shown = true; notes.Get("note3").Shown = true; basket.FullColored = true; basket.Get("basket three").Shown = true;
            }),
            noteAni,
            momAni,
            seqAni
        );


        return paralAni;
    }

    /*
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
    */
}

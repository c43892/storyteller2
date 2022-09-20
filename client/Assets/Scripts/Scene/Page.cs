using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Page : MonoBehaviour
{
    public Transform ObjectRoot;
    public BaseObjects[] Objects;

    void Awake()
    {
        var arr = new List<BaseObjects>();
        for (var i = 0; i < ObjectRoot.childCount; i++)
        {
            var child = ObjectRoot.GetChild(i).GetComponent<BaseObjects>();
            if (child != null)
                arr.Add(child);
        }

        Objects = arr.ToArray();
    }

    public BaseObjects GetObj(string name) => Objects.First(obj => obj.name == name);

    public void ResetAllZ(int startFrom)
    {
        var n = startFrom;
        Objects.ToList().ForEach(obj => obj.ResetAllZ(ref n));
        startFrom = n;
    }
}

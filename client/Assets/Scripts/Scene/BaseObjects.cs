using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseObjects : MonoBehaviour
{
    public string Name { get => transform.gameObject.name; }

    public GameObject ColoredGo;
    public GameObject UncoloredGo;
    public BaseObjects[] SubObjects;

    private void Awake()
    {
        // grab colored & uncolored parts
        var tr = transform.Find("colored");
        ColoredGo = tr == null ? null : tr.gameObject;
        tr = transform.Find("uncolored");
        UncoloredGo = tr == null ? null : tr.gameObject;

        // grab all sub objects
        var arr = new List<BaseObjects>();
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<BaseObjects>();
            if (child != null)
                arr.Add(child);
        }

        SubObjects = arr.ToArray();
    }

    public bool FullColored
    {
        get => (ColoredGo == null || ColoredGo.activeSelf) && SubObjects.All(obj => obj.FullColored);
        set
        {
            if (ColoredGo != null) ColoredGo.SetActive(value);
            if (UncoloredGo != null) UncoloredGo.SetActive(!value);

            SubObjects.ToList().ForEach(obj => obj.FullColored = value);
        }
    }

    public bool FullUncolored
    {
        get => (UncoloredGo == null || UncoloredGo.activeSelf) && SubObjects.All(obj => obj.FullUncolored);
        set
        {
            if (ColoredGo != null) ColoredGo.SetActive(!value);
            if (UncoloredGo != null) UncoloredGo.SetActive(value);

            SubObjects.ToList().ForEach(obj => obj.FullUncolored = value);
        }
    }

    public bool Shown
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    public void SetColored(string subObjName, bool colored)
    {
        var obj = SubObjects.First(obj => obj.name == subObjName);
        if (obj != null)
        {
            if (colored)
                obj.FullColored = true;
            else
                obj.FullUncolored = true;
        }
    }

    public BaseObjects Get(string name) => SubObjects.First(obj => obj.name == name);
    public IEnumerable<BaseObjects> GetAll(params string[] names) => SubObjects.Where(obj => names.Contains(obj.name));

    public void ResetAllZ(ref int startFrom)
    {
        var n = startFrom;
        transform.position = new Vector3(transform.position.x, transform.position.y, n--);

        if (ColoredGo != null) ColoredGo.transform.position = new Vector3(ColoredGo.transform.position.x, ColoredGo.transform.position.y, n--);
        if (UncoloredGo != null) UncoloredGo.transform.position = new Vector3(UncoloredGo.transform.position.x, UncoloredGo.transform.position.y, n--);

        SubObjects.ToList().ForEach(obj => obj.ResetAllZ(ref n));

        startFrom = n;
    }
}

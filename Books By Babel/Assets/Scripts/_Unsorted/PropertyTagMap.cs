using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropertyTagMap <T,G>
{
    Dictionary<Tuple<string, string>, Tuple<T, G>> tileEffectMap;

    public List<string> propertyList;


    public PropertyTagMap()
    {
        tileEffectMap = new Dictionary<Tuple<string, string>, Tuple<T, G>>();
        propertyList = new List<string>();
    }


    public Tuple<T, G> GetEffect(Tuple<string, string> tuple)
    {
        return tileEffectMap[tuple];
    }


    public Tuple<T, G> GetEffect(string key1, string key2)
    {
        Tuple<string, string> t = new Tuple<string, string>(key1, key2);

        return GetEffect(t);
    }


    public bool EntryExists(Tuple<string, string> t)
    {
        return tileEffectMap.ContainsKey(t);
    }


    public bool EntryExists(string key1, string key2)
    {
        return EntryExists(Tuple<string, string>.GenerateTuple(key1, key2));
    }


    public void AddKey(string key1, string key2, T i, G effect)
    {
        tileEffectMap.Add(Tuple<string, string>.GenerateTuple(key1, key2), new Tuple<T, G>(i, effect));
        //tileEffectMap.Add(Tuple<string, string>.GenerateTuple(key2, key1), new Tuple<T, G>(i, effect));
    }


    public void AddProperty(string k)
    {
        if(CheckIfValidProperty(k) == false)
        {
            propertyList.Add(k);
        }
    }


    public bool CheckIfValidProperty(string k)
    {
        return propertyList.Contains(k);
    }

}

[System.Serializable]
public struct Tuple <T, G>
{
    public T ele1;
    public G ele2;

    public Tuple(T ele1, G ele2 )
    {
        this.ele1 = ele1;
        this.ele2 = ele2;
    }

    public static Tuple<string, string> GenerateTuple(string ele1, string ele2)
    {
        Tuple<string, string> t = new Tuple<string, string>(ele1, ele2);

        return t;
    }
}

public class ReferenceTuple<T, G>
{
    public T ele1;
    public G ele2;

    public ReferenceTuple(T ele1, G ele2)
    {
        this.ele1 = ele1;
        this.ele2 = ele2;
    }
}



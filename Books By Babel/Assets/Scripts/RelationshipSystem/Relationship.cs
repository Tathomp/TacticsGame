using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Relationship
{
    //the other actore in the relationship and the value of that relationship
    Dictionary<string, int> RelationshipDict;
    Dictionary<string, int> MaxValueDict;


    public Relationship()
    {
        RelationshipDict = new Dictionary<string, int>();
        MaxValueDict = new Dictionary<string, int>();
    }

    public bool HasRelationship(string actor)
    {
        return RelationshipDict.ContainsKey(actor);
    }

    public int GetRelationship(string actor)
    {
        return RelationshipDict[actor];
    }

    public void AddRelationship(string s, int x)
    {
        int newValue = RelationshipDict[s];
        newValue += x;

        if(newValue > MaxValueDict[s])
        {
            newValue = MaxValueDict[s];
        }

        RelationshipDict[s] = newValue;
    }


    public void SetRelationship(string s, int x, int maxValue = 300)
    {
        RelationshipDict.Add(s, x);
        MaxValueDict.Add(s, maxValue);
    }


    public string[] GetRelationshipKeys()
    {
        return RelationshipDict.Keys.ToArray();
    }

    public string GetRelationshipScore(string key)
    {
        string v = "";

        if (HasRelationship(key))
        {
            int rvalue = GetRelationship(key);

            if (rvalue == 0)
            {
                v = "---";
            }
            else if (rvalue < 100)
            {
                v = "C";
            }
            else if (rvalue < 200)
            {
                v = "B";
            }
            else if(rvalue < 300)
            {
                v = "A";
            }
            else
            {
                v = "S";
            }
        }

        return v;
    }

    public List<Tuple<string, string>> GetAllRelationshipScores()
    {
        List<Tuple<string, string>> rels = new List<Tuple<string, string>>();

        foreach (string k in GetRelationshipKeys())
        {
            Tuple<string, string> t = new Tuple<string, string>();
            t.ele1 = k;
            t.ele2 = GetRelationshipScore(k);
            rels.Add(t);
        }


        return rels;
    }

    public List<Tuple<string, int>> GetAllRelationships()
    {
        List<Tuple<string, int>> rels = new List<Tuple<string, int>>();

        foreach (string k in GetRelationshipKeys())
        {
            Tuple<string, int> t = new Tuple<string, int>();
            t.ele1 = k;
            t.ele2 = RelationshipDict[k];
            rels.Add(t);
        }


        return rels;
    }

    public Relationship Copy()
    {
        Relationship r = new Relationship();

        List<Tuple<string, int>> t = GetAllRelationships();

        foreach (Tuple<string, int> tuples in t)
        {
            r.SetRelationship(tuples.ele1, tuples.ele2, MaxValueDict[tuples.ele1]);
        }

        return r;
    }
}

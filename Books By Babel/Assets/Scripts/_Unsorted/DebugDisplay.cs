using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DebugDisplay : MonoBehaviour
{
    public TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpDateList(List<float> scores)
    {
        string t = "";

        foreach (float item in scores)
        {
            t += item + "\n";
        }

        text.text = t;
    }


    public void UpdateDisplay(Dictionary<string, float> dic)
    {
        string t = "";

        foreach (string key in dic.Keys.ToArray())
        {
            t += key + "\t" + dic[key] + "\n";
        }

        text.text = t;
    }
}

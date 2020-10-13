using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPanel : MonoBehaviour
{
    //public static TutorialPanel instance;
    public TMP_Text textbox;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }*/
    }

    public void UpdateText(string s)
    {
        textbox.text = s;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources/Color/ColorGlossary", menuName = "ScriptableObjects/ColorGlossary")]
public class ColorDatabase : ScriptableObject
{
    public List<ColorDatabaseEntry> colorEntries;


    public ColorDatabaseEntry GetEntry(string s)
    {
        foreach (ColorDatabaseEntry entry in colorEntries)
        {
            if(entry.key == s)
            {
                return entry;
            }
        }


        return null;
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Resources/Color/ColorEntry", menuName = "ScriptableObjects/ColorEntry")]
public class ColorDatabaseEntry : ScriptableObject
{
    public List<Color> colors;
    public string key;



}

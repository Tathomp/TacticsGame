using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileSelectionPanel : MonoBehaviour {

    public TMP_Text selectorText;



    public void UpdateText(Selector selector)
    {

        selectorText.text = "";

        TileNode n = selector.nodeSelected;
        string temp = "X: " + selector.mapPosX + " Y: " + selector.mapPosY + "\n";

        temp  += "Tile: " + selector.nodeSelected.type.TileName + "\n";

        foreach (string a in selector.nodeSelected.type.attributes)
        {
            temp += a + "\n";
        }

        StatsContainer sc = n.type.tileBonuses;

        temp += sc.PrintStats();




        foreach (TileEffect e in n.tileEffects)
        {
            temp += e.GetDescription() + "\n";
        }

        selectorText.text = temp;
    }
}

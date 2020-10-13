using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldMapLocationInfoPanel : MonoBehaviour
{
    public TMP_Text info;
    private WorldMapLocationGameObject currentNodeGO;
    private LocationNode currentNode;

    public void UpdataLocationNodeInfo(WorldMapLocationGameObject node)
    {
        if(node == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        currentNodeGO = node;
        currentNode = node.location;


        PrintInfo();
    }

    private void PrintInfo()
    {
        string s = currentNode.AreaName +"\n";
        s += currentNode.FlavorText + "\n";

        foreach (LocationComponent component in currentNode.locationcomponents)
        {
            s += component.GetDescription();
        }

        if (currentNodeGO.missions.Count > 0)
        {
            s += "\n" + "Missions: " + "\n";

            foreach (Mission miss in currentNodeGO.missions)
            {
                s += miss.MissionName + "\n";
            }
        }

        info.text = s;
    }

}

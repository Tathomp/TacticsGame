using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicEventPanel : MonoBehaviour
{
    //ui
    public ScrollListScaleableContent eventContainer;
    public PublicEventDisplayObject displayobjPrefab;
    public BoardManager bm;
    //private
    private List<PublicEventDisplayObject> displayObjList = new List<PublicEventDisplayObject>();


    public void InitDisplay()
    {
        Mission  mission = bm.currentMission;
        ClearList();

        DisplayEvents(mission.MissionEvents);


    }


    private void DisplayEvents(List<Event> events)
    {
        bool turnon = false;

        foreach (Event e in events)
        {
            if(e.publicEvent)
            {
                PublicEventDisplayObject eventDisplayObject = Instantiate<PublicEventDisplayObject>(displayobjPrefab, eventContainer.contentTransform);
                displayObjList.Add(eventDisplayObject);
                eventDisplayObject.InitDisplayObject(e);

                turnon = true;
            }
        }

        gameObject.SetActive(turnon);

    }



    public void ClearList()
    {
        int x = displayObjList.Count - 1;

        for (int i = x; i >= 0; i--)
        {
            Destroy(displayObjList[i].gameObject);
            Destroy(displayObjList[i]);
        }


        displayObjList = new List<PublicEventDisplayObject>();
    }

    

}

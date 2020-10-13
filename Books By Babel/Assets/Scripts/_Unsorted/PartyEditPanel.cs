using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyEditPanel : MonoBehaviour
{

    public ScrollListScaleableContent actorlist;
    public TextButton buttonPrefab;



    public CharacterInfoDisplayPanel stats;
    public TalentPanelManager talents;
    public EquipmentPanel equipment; //these two classes should be cleaned up

   // private CharacterInfoDisplayPanel currentScreen;
    public ActorData currentData;

   


    public void InitPanel(ActorData d)
    {

        InitPanel();

        PopulateCurrentDisplay(d);
    }

    public void InitPanel()
    {

        gameObject.SetActive(true);

        CleanUp();

        foreach (ActorData d in Globals.campaign.currentparty.partyCharacter)
        {
            TextButton temp = Instantiate<TextButton>(buttonPrefab, actorlist.contentTransform);
            temp.ChangeText(d.Name);
            temp.button.onClick.AddListener(delegate { PopulateCurrentDisplay(d); });
            actorlist.AddToList(temp);
        }

        actorlist.buttonConatiner.ClickSelectedButton(0);
    }

    public void PopulateCurrentDisplay(ActorData data)
    {
        currentData = data;
        stats.PopulatePanel(data);
        gameObject.SetActive(true); 
    }

    public void SwapToStatDispla()
    {
        SwapPanel(stats);
    }

    public void SwapToTalentPanel()
    {
        SwapPanel(talents);
    }

    public void SwapToEquipment()
    {
        SwapPanel(equipment);
    }

    public void SwapPanel(CharacterInfoDisplayPanel newDisplay)
    {
        if(stats != null)
        {
            stats.gameObject.SetActive(false);
        }

        stats = newDisplay;
        stats.PopulatePanel(currentData);
    }


    void CleanUp()
    {
        actorlist.CleanUp();
        
    }

    public void ToggleOff()
    {
        CleanUp();
        gameObject.SetActive(false);
    }
}

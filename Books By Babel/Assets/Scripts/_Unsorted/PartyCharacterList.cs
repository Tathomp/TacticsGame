using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Delete this probablys
public class PartyCharacterList : MonoBehaviour
{
    public ScrollListScaleableContent actorlist;
    public TextButton buttonPrefab;


    public CharacterInfoDisplay infoDisplay;
    public TalentPanelManager talentDisplay;
    public EquipmentPanel equipmentDisplay;

    private ActorData currActor;
    private GameObject currentMenu;

    public void Populate()
    {
        actorlist.CleanUp();

        foreach (ActorData d in Globals.campaign.currentparty.partyCharacter)
        {

            TextButton temp = Instantiate<TextButton>(buttonPrefab, actorlist.contentTransform);
            temp.ChangeText(d.Name);
            temp.button.onClick.AddListener(delegate { ActorButtonClicked(d); });
            actorlist.AddToList(temp);
        }

        actorlist.AdjustContentLength();
    }

    public void SwapDisplay(GameObject go)
    {
        if(currentMenu != null)
        currentMenu.SetActive(false);
        currentMenu = go;
        currentMenu.SetActive(true);

    }

    public void DisplayData()
    {
        infoDisplay.PopulatePanel(currActor);
        //SwapDisplay(infoDisplay);
    }

    public void ActorButtonClicked(ActorData data)
    {
        infoDisplay.PopulatePanel(data);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

    //Prefab
    public Button prefab;

    //UI Stuff
    public Transform partyInventoryContainer, actorInventyContainer;
    public Text itemInfoText;

    //private memebers
    ActorData currActor;
    List<Button> actorInvButtons, partyinvButtons;
    Inventory partyInventory, actorInventory;

    public void ToggleOn(ActorData data)
    {
        currActor = data;
        gameObject.SetActive(true);

        partyInventory = Globals.campaign.currentparty.partyInvenotry;
        actorInventory = data.inventory;

        UpdateList();
    }

    void PopulateActorInventory()
    {
        List<ItemContainer> items = actorInventory.ItemSlots;

        for (int i = 0; i < items.Count - 1; i++)
        {
            if(items[i] != null)
            {
                Item it = Globals.campaign.GetItemData(items[i].itemKey);
                Button b = Instantiate<Button>(prefab, actorInventyContainer);

                b.transform.GetChild(0).GetComponent<Text>().text = it.Name;
                actorInvButtons.Add(b);
                b.onClick.AddListener(delegate { ActorButtonClicked(it); } );
            }
        }
    }

    public void ActorButtonClicked(Item item)
    {
        itemInfoText.text = item.descript;
    }

    public void InventoryButtonClicked(Item item)
    {
        itemInfoText.text = item.descript;
    }

    void PopulatePartyInventory()
    {
        List<ItemContainer> items = partyInventory.ItemSlots;

        for (int i = 0; i < items.Count - 1; i++)
        {
            if (items[i] != null)
            {
                Item it = Globals.campaign.GetItemData(items[i].itemKey);
                Button b = Instantiate<Button>(prefab, partyInventoryContainer);

                b.transform.GetChild(0).GetComponent<Text>().text = it.Name;
                partyinvButtons.Add(b);
                b.onClick.AddListener(delegate { InventoryButtonClicked(it); });

            }
        }
    }

    void UpdateList()
    {
        CleanButtons();


        actorInvButtons = new List<Button>();
        partyinvButtons = new List<Button>();

        PopulateActorInventory();
        PopulatePartyInventory();

    }

    void CleanButtons()
    {
        if(actorInvButtons == null || partyInventory == null)
        {
            return;
        }

        for (int i = actorInvButtons.Count - 1; i >= 0; i--)
        {
            actorInvButtons[i].onClick.RemoveAllListeners();
            GameObject.Destroy(actorInvButtons[i].gameObject);
            GameObject.Destroy(actorInvButtons[i]);

        }


        for (int i = partyinvButtons.Count - 1; i >= 0; i--)
        {
            partyinvButtons[i].onClick.RemoveAllListeners();
            GameObject.Destroy(partyinvButtons[i].gameObject);
            GameObject.Destroy(partyinvButtons[i]);

        }
    }

    private void OnDisable()
    {
        CleanButtons();
        actorInvButtons = new List<Button>();
        partyinvButtons = new List<Button>();
    }

}

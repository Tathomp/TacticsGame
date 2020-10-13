using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TODO: DELETE probably
/// </summary>
public class CharacterSelectInfoPanel : MonoBehaviour {

    public BoardManager boardManager;
    public Image portrait;
    public TMP_Text statText;

    public Dropdown primarySelection;
    public Dropdown secondarySelection;

    public TMP_Text headSlot;
    public TMP_Text weapSlot;
    public TMP_Text bodyslot;

    private ActorData currActor;
    public TMP_Text unitsPlacedLabel;


    public void SelectedActor(ActorData data)
    {

        portrait.sprite = Globals.GetSprite(FilePath.ActorSpriteAtlas, data.portraitFilePath);
        PrintJobs(data);
        PrintEquipement(data);
        PrintStats(data);
        InitDropDowns(data);
        currActor = data;

    }


    void PrintJobs(ActorData data)
    {
        Job primary = Globals.campaign.GetJobsData().JobDB.GetCopy(data.primaryJob);

        primarySelection.onValueChanged.RemoveAllListeners();
        primarySelection.onValueChanged.AddListener(delegate { PrimaryValueChange(data); });

        List<Job> jobs = Globals.campaign.GetJobsData().GetJobsAvaliableData(data);

        if(primary != null)
        {
            primarySelection.options.Clear();

            for (int i = 0; i < jobs.Count; i++)
            {
                if(jobs[i].JobUnlocked(data))
                {
                    primarySelection.options.Add(new Dropdown.OptionData(jobs[i].Name));
                }
            }
        }

        secondarySelection.onValueChanged.RemoveAllListeners();
        secondarySelection.onValueChanged.AddListener(delegate { SecondaryValueChange(data); });

        secondarySelection.options.Clear();

        if (data.secondaryJob == "")
        {
            secondarySelection.options.Add(new Dropdown.OptionData("---"));
        }

       
           // Job secondary = Globals.campaign.GetJobsData().JobDB.GetCopy(data.secondaryJob);

            for (int i = 0; i < jobs.Count; i++)
            {
                if (jobs[i].JobUnlocked(data))
                {
                    secondarySelection.options.Add(new Dropdown.OptionData(jobs[i].Name));
                }
            }

        
    }


    void SecondaryValueChange(ActorData data)
    {
        int v = secondarySelection.value;

        foreach (Job job in Globals.campaign.GetJobsData().GetJobsAvaliableData(data))
        {
            if (job.Name == secondarySelection.options[v].text)
            {
                data.secondaryJob = job.GetKey();
                PrintStats(data);

                return;
            }
        }
        data.secondaryJob = "";
        PrintStats(data);

    }


    void PrintEquipement(ActorData data)
    {
        if (data.equipment.GetHeadItem() != "")
        {
            headSlot.text = Globals.campaign.GetItemData(data.equipment.GetHeadItem()).Name;
        }

        if (data.equipment.GetPrimaryWeapon() != "")
        {
            weapSlot.text = Globals.campaign.GetItemData(data.equipment.GetPrimaryWeapon()).Name;
        }

        if (data.equipment.GetBodyItem() != "")
        {
            bodyslot.text = Globals.campaign.GetItemData(data.equipment.GetBodyItem()).Name;
        }
    }


    void PrintStats(ActorData data)
    {
        string temp = "Name: ";


        temp += data.Name + "\n";
        temp += "Level: " + data.Level + "\n";
        temp += "Exp: " + data.XP + "\n";
        temp += "Job: " + Globals.campaign.GetJobsData().JobDB.GetCopy(data.primaryJob).Name + "\n";

        if (data.HasSecondaryJob())
        {
            temp += "Secondary: " + Globals.campaign.GetJobsData().JobDB.GetCopy(data.secondaryJob).Name + "\n";
        }
        else
        {
            temp += "Secondary: " + "---" + "\n";

        }

        StatTypes[] keys = data.maxStatCollection.GetKeys();

        foreach (StatTypes key in keys)
        {
            temp += key.ToString() + " " + data.maxStatCollection.statDict[key] + "\n";
        }


        statText.text = temp;
    }


    void PrimaryValueChange(ActorData data)
    {
        int v = primarySelection.value;
        foreach (Job job in Globals.campaign.GetJobsData().GetJobsAvaliableData(data))
        {
            if(job.Name  == primarySelection.options[v].text)
            {
                data.primaryJob = job.GetKey();
                PrintStats(data);
            }
        }
    }


    void InitDropDowns(ActorData data)
    {
        for (int i = 0; i < primarySelection.options.Count; i++)
        {
            if(primarySelection.options[i].text == Globals.campaign.GetJobsData().JobDB.GetCopy(data.primaryJob).Name)
            {
                primarySelection.value = i;
                break;
            }
        }

        if(data.HasSecondaryJob() == false)
        {
            secondarySelection.value = 0;
            return;
        }

        for (int i = 0; i < secondarySelection.options.Count; i++)
        {
            if (secondarySelection.options[i].text == Globals.campaign.GetJobsData().JobDB.GetCopy(data.secondaryJob).Name)
            {
                secondarySelection.value = i;
                break;
            }
        }
    }


    public void StartFight()
    {
        boardManager.ui.TurnInfoPanels();

        if(boardManager.party.NumberOfSelected() == 0)
        {
            //Prevents the user from starting the game with zero units deployed
            return;
        }

        boardManager.inputFSM.SwitchState(new UsersTurnState(boardManager));
        boardManager.turnManager.CalculateFastest();
    }

    public void UpDatePlacementNumber(int c, int m)
    {
        unitsPlacedLabel.text = "Units Deployed: " + c + "\\" + m;
    }
}

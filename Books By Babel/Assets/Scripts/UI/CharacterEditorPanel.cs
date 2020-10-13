using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

//TODO delete this 

public class CharacterEditorPanel : MonoBehaviour {

    public Image characterPortrait;

    public TMP_Text characterName, level, xp, stats, relationships;

    public Dropdown primaryJob, secondaryJob;

    private ActorData data;


    public TalentPanelManager talentPanel;
    public EquipmentPanel equipmentPanel;
   // public InventoryPanel inventoryPanel;


    public void ToggleOn()
    {

        talentPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
        //inventoryPanel.gameObject.SetActive(false);


        Party currParty = Globals.campaign.currentparty;



        gameObject.SetActive(true);

        ActorData data = currParty.partyCharacter[0];


        PopulateEditorPanel(data);
    }


    void ActorButtonClicked(ActorData data)
    {
        PopulateEditorPanel(data);
    }

    private void OnDisable()
    {
        ToggleOff();
    }

    public void ToggleOff()
    {
        CleanUpListeners();
        ///This really shouldn't be nessacry
        Party temp = Globals.campaign.currentparty;

        for (int i = 0; i < temp.partyCharacter.Count; i++)
        {
            if(data.Name == temp.partyCharacter[i].Name)
            {
                temp.partyCharacter[i] = data;

                return;
            }
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }


    public void DisplayTalentScreen()
    {
        talentPanel.PopulatePanel(data);
        this.gameObject.SetActive(false);
    }

    public void DisplayEquipmentScreen()
    {
        equipmentPanel.PopulatePanel(data);
        this.gameObject.SetActive(false);
    }

    public void DisplayInventoryScreen()
    {
        //inventoryPanel.ToggleOn(data);
        this.gameObject.SetActive(false);
    }

	void PopulateEditorPanel(ActorData data)
    {
        this.data = data;

        RelationshipData(data);
        UpdatePortrait(data);
        UpdateCoreTexts(data);
        PrintStats(data);
        InitDropDowns(data);
        JobAndRace();
    }


    void UpdateCoreTexts(ActorData data)
    {
        characterName.text = data.Name;
        level.text = "Level: " + data.Level.ToString();
        xp.text = "XP: " + data.XP.ToString();
    }


    void UpdatePortrait(ActorData data)
    {
        characterPortrait.sprite = Resources.Load<SpriteAtlas>(FilePath.ActorSpriteAtlas).GetSprite(data.portraitFilePath);
    }

    void RelationshipData(ActorData data)
    {

        //List<Tuple<string, int>> rels = data.Relationships.GetAllRelationships();
        string s = "";
        /*
        foreach (Tuple<string, int> rel in rels)
        {
            s += rel.ele1 + ": " + rel.ele2 + "\n";
        }
            */
        s = data.maxStatCollection.PrintStats();

        relationships.text = s;

    }


    #region Job Display
    void UpdateJobs(ActorData data)
    {
        CleanJobs();

        primaryJob.options.Clear();
        primaryJob.onValueChanged.AddListener(delegate { PrimaryJobChanged(data); } );

        List<Job> jobs = Globals.campaign.GetJobsData().GetJobsAvaliableData(data);

        for (int i = 0; i < jobs.Count; i++)
        {
            //if there is no secondary job, exception is thrown
            if(data.secondaryJob != null)
            {
                if (jobs[i].JobUnlocked(data) && data.secondaryJob != jobs[i].GetKey())
                {
                    primaryJob.options.Add(new Dropdown.OptionData(jobs[i].Name));
                }
            }
            else
            {
                if (jobs[i].JobUnlocked(data))
                {
                    primaryJob.options.Add(new Dropdown.OptionData(jobs[i].Name));
                }
            }

        }

        secondaryJob.onValueChanged.AddListener(delegate { SecondaryJobChanged(data); });

        
            secondaryJob.options.Clear();

            secondaryJob.options.Add(new Dropdown.OptionData("---"));

            for (int i = 0; i < jobs.Count; i++)
            {
                if (jobs[i].JobUnlocked(data) && Globals.campaign.GetJobsData().JobDB.GetCopy(data.primaryJob).Name != jobs[i].Name)
                {
                    secondaryJob.options.Add(new Dropdown.OptionData(jobs[i].Name));
                }
            }

        

    }


    void PrimaryJobChanged(ActorData data)
    {
        int v = primaryJob.value;

        List<Job> jobs = Globals.campaign.GetJobsData().GetJobsAvaliableData(data);


        foreach (Job job in jobs)
        {
            if (job.Name == primaryJob.options[v].text)
            {
                data.ChangeJobs(job.GetKey(), SwitchJobsEffect.JobCategory.Primary);
                Debug.Log("Job changed");
            }
        }

        UpdateJobs(data);
    }

    void SecondaryJobChanged(ActorData data)
    {
        int v = secondaryJob.value;

        List<Job> jobs = Globals.campaign.GetJobsData().GetJobsAvaliableData(data);

        if (v == 0)
        {
            //no job selected
            data.secondaryJob = "";
            return;
        }

        foreach (Job job in jobs)
        {
            if (job.Name == secondaryJob.options[v].text)
            {
                data.ChangeJobs(job.GetKey(), SwitchJobsEffect.JobCategory.Secondary);
                Debug.Log("Job changed");
            }
        }

        UpdateJobs(data);

    }


    void InitDropDowns(ActorData data)
    {
        UpdateJobs(data);

        for (int i = 0; i < primaryJob.options.Count; i++)
        {
            if (primaryJob.options[i].text == Globals.campaign.GetJobsData().JobDB.GetCopy(data.primaryJob).Name)
            {
                primaryJob.value = i;
                break;
            }
        }

        Debug.Log(data);

        for (int i = 0; i < secondaryJob.options.Count; i++)
        {
            if (data.HasSecondaryJob())
            {
                if (secondaryJob.options[i].text == Globals.campaign.GetJobsData().JobDB.GetCopy(data.secondaryJob).Name)
                {
                    secondaryJob.value = i;
                    break;
                }
            }
            else
            {
                secondaryJob.value = 0;
            }
        }

        primaryJob.RefreshShownValue();
        secondaryJob.RefreshShownValue();
    }

    #endregion


    void PrintStats(ActorData data)
    {
        StatTypes[] keys = data.maxStatCollection.GetKeys();

        string temp ="";

        temp += data.currentStatCollection.statDict[StatTypes.Health] +"\n";

        foreach (StatTypes key in keys)
        {
            temp += key.ToString() + " " + data.maxStatCollection.statDict[key] + "\n";
        }


        stats.text = temp;
    }

    void CleanJobs()
    {
        primaryJob.onValueChanged.RemoveAllListeners();
        secondaryJob.onValueChanged.RemoveAllListeners();
    }


    void CleanUpListeners()
    {
        CleanJobs();
    }


    public void JobAndRace()
    {
        string temp = "Tags: \n" ;

        temp += data.race + "\n";
        temp += data.primaryJob + "\n";
        temp += data.secondaryJob + "\n";

        foreach (string d in data.actorPropertyTags)
        {
            temp += d + "\n";
        }

        stats.text = temp;
    }

    public void QuitToCharacterPanel()
    {
        ToggleOn();
    }
}

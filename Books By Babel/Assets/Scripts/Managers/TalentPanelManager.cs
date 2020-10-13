using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentPanelManager : CharacterInfoDisplayPanel
{

    public TextButton jobButton;
    public ScrollListScaleableContent jobContainer, talentContainer;
    public TMP_Text skillInfo, jobPointLabel, jobName, jobDescription, currentJobs;
    public TextButton LearnSkillButton;

    public ActorData currActor;
    public Job currentJob;

    List<Job> availiableJobs = new List<Job>();
    List<TextButton> jobbuttons = new List<TextButton>();
    List<TextButton> talentButtons = new List<TextButton>();

    

    private void OnDisable()
    {
        CleanButtons();
        ClearTalentButtons();
        LearnSkillButton.button.onClick.RemoveAllListeners();

        SaveStateBase state = new SaveStateBase(Globals.campaign);
        SaveLoadManager.AutoSaveCampaignProgress(state);
    }

    public void SwapPrimaryJob()
    {
        currActor.ChangeJobs(currentJob.GetKey(), SwitchJobsEffect.JobCategory.Primary);

        PrintCurrentJobs();

    }


    public void SwapSecondaryJob()
    {
        currActor.ChangeJobs(currentJob.GetKey(), SwitchJobsEffect.JobCategory.Secondary);

        PrintCurrentJobs();

    }

    public override void PopulatePanel(ActorData actordata)
    {
        gameObject.SetActive(true);

        availiableJobs = new List<Job>();
        jobbuttons = new List<TextButton>();
        talentButtons = new List<TextButton>();

        PopulateTalentPanel(actordata);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }

    void PrintCurrentJobs()
    {
        currentJobs.text = "Primary Job: " + currActor.primaryJob + "\n" + "Secondary job: " + currActor.secondaryJob;

    }

    public void PopulateTalentPanel(ActorData actorData)
    {

        currActor = actorData;

        PrintCurrentJobs();

        availiableJobs = currActor.ProcessJobUnlocks();

        foreach (Job job in availiableJobs)
        {
            TextButton temp = Instantiate<TextButton>(jobButton, jobContainer.contentTransform);
            temp.ChangeText(job.Name);
            temp.button.onClick.AddListener(delegate { DisplayAbilities(job); });
            jobbuttons.Add(temp);
        }

        if(jobbuttons.Count > 0)
        {
            jobbuttons[0].button.onClick.Invoke();
        }

        jobContainer.AdjustContentLength();
    }


    public void CleanButtons()
    {
        if (jobbuttons == null)
            return;

        int count = jobbuttons.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            jobbuttons[i].button.onClick.RemoveAllListeners();
            Destroy(jobbuttons[i].gameObject);
            Destroy(jobbuttons[i]);

        }

        jobbuttons = new List<TextButton>();
    }


    public void ClearTalentButtons()
    {
        if (talentButtons == null)
            return;

        int count = talentButtons.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            talentButtons[i].button.onClick.RemoveAllListeners();
            Destroy(talentButtons[i].gameObject);
            Destroy(talentButtons[i]);
        }

        talentButtons = new List<TextButton>();
    }


    public void DisplayAbilities(Job job)
    {
        ClearTalentButtons();

        currentJob = job;

        jobName.text = job.Name;
        jobDescription.text = job.descript;
        PrintJobLabel(job);



        foreach (Discipline disc in currentJob.avalibleDisciples)
        {
          //  Button discButton = Instantiate<Button>(jobButton, talentContainer);
            //discButton.transform.GetChild(0).GetComponent<Text>().text = "Discipline: " + disc.Name;
            //talentButtons.Add(discButton);

            foreach (string key in disc.TalenPool)
            {
                Talent skill = Globals.campaign.contentLibrary.TalentDB.GetCopy(key);

                TextButton temp = Instantiate<TextButton>(jobButton, talentContainer.contentTransform);
                temp.ChangeText( skill.TalentNodename);
                talentButtons.Add(temp);

                temp.button.onClick.AddListener(delegate { TalentClicked(skill); });

                if (currActor.JobDataState.SkillLearned(currentJob.GetKey(), skill.GetKey()) == true)
                {
                    temp.button.interactable = false;
                }

            }
        }
        talentContainer.AdjustContentLength();

        if (talentButtons.Count>0)
        talentButtons[0].button.onClick.Invoke();

    }

    void PrintJobLabel(Job job)
    {
        string s = "Total Job Points: " + currActor.JobDataState.JobPoints[currentJob.GetKey()] + "\n\n";
        s += job.descript;


        jobPointLabel.text = s;
    }

    void PrintSkillInfo(Talent skill)
    {
        string t = "";

        t += "Name:" + skill.TalentNodename + "\n\n";
        t += "Description: " + skill.GetDescription() + "\n\n";

        if(currActor.JobDataState.SkillLearned(currentJob.GetKey(), skill.GetKey()))
        {
            t += "Already leanred!";
        }
        else
        {
            t += "Cost to learn: " + skill.skillCost;
            LearnSkillButton.button.onClick.RemoveAllListeners();
            LearnSkillButton.button.onClick.AddListener(delegate { LearnButton(skill); });
        }

        skillInfo.text = t;
    }

    public void LearnButton(Talent skill)
    {
        if (currActor.JobDataState.SkillLearned(currentJob.GetKey(), skill.GetKey()) == false)
        {
            currActor.JobDataState.JobPoints[currentJob.GetKey()] -= skill.skillCost;


            currActor.LearnTalent(currentJob.GetKey(), skill);

            Debug.Log(skill.TalentNodename + "leanred!");
            //PrintSkillInfo(skill);
            //PrintJobLabel(currentJob);
            gameObject.SetActive(false);
            PopulatePanel(currActor);
        }
    }

    public void TalentClicked(Talent skill)
    {
        Debug.Log(skill.TalentNodename);
        PrintSkillInfo(skill);
    }
}

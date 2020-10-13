using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectMenuPanel : MonoBehaviour
{
    // public abstract
    public CreationSuiteManager manager;

    public abstract void InitPanel(BuffEffect effect);
    protected abstract void Save();
    protected abstract void CleanUp();

    private void OnDisable()
    {
        CleanUp();
    }


    public void PopulateBuffSelections(string current, DropdownMenu dropDown)
    {
        PopulateMenu(manager.currentCampaign.contentLibrary.buffDatabase.DbKeys(), current, dropDown);

    }


    public void PopulateSkilJobList(string current, string job, DropdownMenu dropDown)
    {
        PopulateMenu(manager.currentCampaign.GetJobsData().JobDB.GetData(job).GetTalentKeys(), current, dropDown);

    }


    public void PopulateJobList(string current, DropdownMenu dropDown)
    {
        PopulateMenu(manager.currentCampaign.GetJobsData().JobDB.DbKeys(), current, dropDown);

    }


    public void PopulateSkillList(string current, DropdownMenu dropDown)
    {
        PopulateMenu(manager.currentCampaign.contentLibrary.skillDatabase.DbKeys(), current, dropDown);

    }


    public void PopulateProperties(string current, DropdownMenu menu)
    {
        PopulateMenu(manager.currentCampaign.properties, current, menu);
    }

    public void PopulateStatsContainerType(string curren, DropdownMenu menu)
    {
        List<string> temp = new List<string>();

        foreach (StatContainerType item in Enum.GetValues(typeof(StatContainerType)))
        {
            temp.Add(item.ToString());
        }

        PopulateMenu(temp, curren, menu);
    }


    public void POpulateStatTypes(string curren, DropdownMenu menu)
    {
        List<string> temp = new List<string>();

        foreach (StatTypes item in Enum.GetValues(typeof(StatTypes)))
        {
            temp.Add(item.ToString());
        }

        PopulateMenu(temp, curren, menu);
    }

    public void PopulateMenu(IEnumerable list, string current, DropdownMenu dropDown)
    {
        dropDown.ClearList();
        dropDown.ClearListeners();

        foreach (string item in list)
        {
            dropDown.AddList(item, current);
        }

        dropDown.droptDown.RefreshShownValue();

        dropDown.droptDown.onValueChanged.AddListener(delegate { Save(); });
    }
}

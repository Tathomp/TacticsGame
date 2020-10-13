using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwitchJobsEffect : BuffEffect
{
    private string newJob, prevJobs;
    private JobCategory switchPrimary;
    private bool learnAllAbilities;

    private List<string> abilitesToLearn;
    public enum JobCategory { Race, Primary, Secondary }

    // Change this to also work with races
    // Use enum for Primary, Seconard, Race
    //
    public SwitchJobsEffect(string newJob, JobCategory switchPrimary, bool learnAllAbilities)
    {
        this.newJob = newJob;
        this.switchPrimary = switchPrimary;
        this.learnAllAbilities = learnAllAbilities;

        abilitesToLearn = new List<string>();

        prevJobs = "";
    }

    public override BuffEffect Copy()
    {
        SwitchJobsEffect e = new SwitchJobsEffect(newJob, switchPrimary, learnAllAbilities);
        e.prevJobs = prevJobs;

        return e;
    }
    

    public override void OnApply(ActorData actor, ActorData source)
    {
        if (learnAllAbilities)
        {
            foreach (Talent skill in Globals.campaign.GetJobsData().JobDB.GetCopy(newJob).GetTotalTalentPool())
            {
                actor.LearnTalent(newJob, skill);
            }
        }


        switch (switchPrimary)
        {
            case JobCategory.Primary:
                {
                    prevJobs = actor.primaryJob;
      
                    actor.ChangeJobs(newJob, JobCategory.Primary);
                    break;
                }
            case JobCategory.Secondary:
                {
                    prevJobs = actor.secondaryJob;
                    actor.ChangeJobs(newJob, JobCategory.Secondary);
                    break;
                }
            case JobCategory.Race:
                {
                    prevJobs = actor.race;
                    actor.ChangeSpecies(newJob);

                    break;
                }
        }


    }


    public override void OnRemove(ActorData actor)
    {
        switch (switchPrimary)
        {
            case JobCategory.Primary:
                {
                    actor.primaryJob = prevJobs;
                    break;
                }
            case JobCategory.Secondary:
                {
                    actor.secondaryJob = prevJobs;
                    break;
                }
            case JobCategory.Race:
                {
                    actor.ChangeSpecies(prevJobs);


                    actor.ChangeSpecies(prevJobs);
                    actor.race = prevJobs;
                    break;
                }
        }
    }


    public override string PrintNameOfEffect()
    {
        return "Switch Job";

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class JobDataState
{
    // <JobName, Points>
    public Dictionary<string, int> JobPoints;

    // <JobName, TalentsLearned>
    public Dictionary<string, List<string>> TalentsLearned;

    public JobDataState()
    {
        JobPoints = new Dictionary<string, int>();
        TalentsLearned = new Dictionary<string, List<string>>();

        TalentsLearned.Add("generic_skills", new List<string>());
    }


    
    public JobDataState Copy()
    {
        JobDataState state = new JobDataState();


        string[] keys = JobPoints.Keys.ToArray();
        string[] jobAndRaceKey = TalentsLearned.Keys.ToArray();


        foreach (string k in keys)
        {
            state.JobPoints[k] = JobPoints[k];
        }

        state.AddJobKeys(jobAndRaceKey.ToList());

        foreach (string k in jobAndRaceKey)
        {
            foreach (string talent in TalentsLearned[k])
            {
                state.LearnSkill(k, talent);
            }
        }

        return state;
    }

    public List<Skill> GetAllSkillsOffCoolDown(Actor actor)
    {
        List<Skill> temp = new List<Skill>();
        ActorData data = actor.actorData;

        foreach (Skill skill in GetAllLearnedSkills(data.race, data.primaryJob, data.secondaryJob))
        {
            if(data.cooldownMap.IsSKillOnCooldown(skill.GetKey()) == false)
            {
                temp.Add(skill);
            }
        }

        return temp;
    }


    public List<Skill> GetAllLearnedSkills(string race, string job1, string job2)
    {
        List<Skill> temp = new List<Skill>();

        // Add races skills
        //Debug.Log("Primary job: " + job1 + "Secondary: " + job2);

        GetTalentList("generic_skills", temp);
        GetTalentList(race, temp);
        GetTalentList(job1, temp);

        if (job2 != "")
        {
            GetTalentList(job2, temp);
        }

        return temp;
    }

    public List<Skill> GetALlLearnedSkillsForJob(string jobKey)
    {
        List<Skill> temp = new List<Skill>();


        List<Talent> talents = Globals.campaign.GetJobsData().JobDB.GetCopy(jobKey).GetTotalTalentPool();

        foreach (Talent talent in talents)
        {
            if(talent.HasSkill())
            {                
                if (TalentsLearned[jobKey].Contains(talent.GetKey()))
                {
                    temp.Add( Globals.campaign.contentLibrary.skillDatabase.GetData(talent.SkillToLearn.GetKey()));
                }
            }
        }


        return temp;
    }

    public List<Talent> GetAllLearnedTalents(string jobKey)
    {
        List<Talent> t = new List<Talent>();


        foreach (string key in TalentsLearned[jobKey])
        {
            t.Add(Globals.campaign.contentLibrary.TalentDB.GetData(key));
        }



        return t;

    }

    public List<string> GetLearnedTalent(string key)
    {
        List<string> t = new List<string>();

        AddJobKeys(key);

        foreach (string item in TalentsLearned[key])
        {
            Talent ta = Globals.campaign.contentLibrary.TalentDB.GetData(item);

            if(ta.HasPassive())
            {
                t.Add( ta.PassiveBuff.GetKey() );
            }

        }


        return t;
    }


    public List<Skill> GetTalentList(string key, List<Skill> skills)
    {
        if (TalentsLearned.ContainsKey(key))
        {
            foreach (string talentLearned in TalentsLearned[key])
            {
                Talent t = Globals.campaign.contentLibrary.TalentDB.GetData(talentLearned);


                if (t.HasSkill())
                {
                    skills.Add(t.SkillToLearn);
                }
            }

            return skills;
        }

        return skills;
    }
    
    public void AddJobKeys(List<string> keys)
    {
        foreach (string k in keys)
        {
            AddJobKeys(k);
        }
    }

    public void AddJobKeys(string key)
    {
        if(TalentsLearned.ContainsKey(key) == false)
        {
            TalentsLearned.Add(key, new List<string>());
        }

        AddJobPoints(key, 400); //default amount of jp to add to a new job
    }

    public void AddJobPoints(string key, int jp)
    {
        if(JobPoints.ContainsKey(key) == false)
        {
            JobPoints.Add(key, jp);
        }
        else
        {
            JobPoints[key] += jp;
        }
    }

    public void UnLearnSkill(string job, string skill)
    {
        TalentsLearned[job].Remove(skill);
    }

    public void LearnSkill(string jobKey, string skillToLearn)
    {
        if(TalentsLearned.ContainsKey(jobKey))
        {
            TalentsLearned[jobKey].Add(skillToLearn);
        }
        else
        {
            AddJobKeys(jobKey);
            LearnSkill(jobKey, skillToLearn);
        }
    }

    public void LearnSkill(string jobKey, List<string> skillsToLearn)
    {
        foreach (string skill in skillsToLearn)
        {
            LearnSkill(jobKey, skill);
        }
    }

    public bool SkillLearned(string jobKey, string skillKey)
    {
        return TalentsLearned[jobKey].Contains(skillKey);
    }

    public string[] GetAllJobsUnlocked()
    {
        return TalentsLearned.Keys.ToArray();
    }
}

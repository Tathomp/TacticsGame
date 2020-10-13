using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class CooldownMap
{
    Dictionary<string, int> cooldowns;

    public CooldownMap()
    {
        cooldowns = new Dictionary<string, int>();
    }

    public CooldownMap Copy()
    {
        CooldownMap tempMap = new CooldownMap();

        return tempMap;
    }

    public void AddSKillToCooldown(IUseable skill)
    {

        //We should never have a key conflict here
        if(cooldowns.ContainsKey(skill.GetKey())==false)
        cooldowns.Add(skill.GetKey(), skill.GetCoolDown());
    }

    public void DecrementAllCoolDowns()
    {
        List<string> ids = cooldowns.Keys.ToList();

        foreach (string id in ids)
        {
            cooldowns[id]--;
            if(cooldowns[id] <=0)
            {
                cooldowns.Remove(id);
            }
        }
    }

    public bool IsSKillOnCooldown(string id)
    {
        return cooldowns.ContainsKey(id);
    }

}


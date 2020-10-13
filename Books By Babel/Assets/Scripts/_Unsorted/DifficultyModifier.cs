using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyModifier
{
    public bool permaDeath;

    //pool
    public List<string> partyBuffPool;
    public List<string> enemyBuffPool;

    //selected
    public List<string> buffToApplyToParty;
    public List<string> buttToApplyTOEnemies;

    public float jp_bonus, xp_bonus, currency_bonus, relationship_bonus;

    public DifficultyModifier()
    {
        partyBuffPool = new List<string>();
        enemyBuffPool = new List<string>();

        buffToApplyToParty = new List<string>();
        buttToApplyTOEnemies = new List<string>();


        jp_bonus = 1f;
        xp_bonus = 1f;
        currency_bonus = 1f;
        relationship_bonus = 1f;
    }
}

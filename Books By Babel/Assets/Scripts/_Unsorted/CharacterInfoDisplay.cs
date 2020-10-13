using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterInfoDisplay : CharacterInfoDisplayPanel
{
    public TMP_Text description, toolTiptext;
    public TMP_Text Name, level, exp, race, pjob, sjob;
    public TMP_Text healt, mana, strength, defense, potentch, resistence,
        speedrating, evade, healthRegen, manaRegen, movementRange;

    public GameObject items;

    public Image portrait;


    public ScrollListScaleableContent tags, relContainer, equipment, inventory, skills, buffs;
    public TMP_Text buttonPrefab;
    public ToolTipPanel infoDisplay;

    //Editing panels
    public EquipmentPanel equipPanel;
    public TalentPanelManager talentPanel;

    private ActorData currActor;

    public override void PopulatePanel(ActorData data)
    {
        currActor = data;

            
            

        tags.CleanUp();
        relContainer.CleanUp();
        equipment.CleanUp();     
        inventory.CleanUp();
        skills.CleanUp();
        buffs.CleanUp();

        Debug.Log(data, this);
        portrait.sprite = Globals.GetSprite(FilePath.ActorSpriteAtlas, data.portraitFilePath);

        healt.text = "Health: " + data.currentStatCollection.GetValue(StatTypes.Health) + " / " + data.maxStatCollection.GetValue(StatTypes.Health);
        mana.text = "Mana: " + data.currentStatCollection.GetValue(StatTypes.Mana) + " / " + data.maxStatCollection.GetValue(StatTypes.Mana);

        strength.text = "Strength: " + data.currentStatCollection.GetValue(StatTypes.Strength);
        defense.text = "Defense: " + data.currentStatCollection.GetValue(StatTypes.Defenese);
        potentch.text = "Potency: " + data.currentStatCollection.GetValue(StatTypes.Potency);
        resistence.text = "Resistence: " + data.currentStatCollection.GetValue(StatTypes.Resistence);
        speedrating.text = "Speed Rating: " + data.currentStatCollection.GetValue(StatTypes.SpeedRating);
        evade.text = "Evade: " + data.currentStatCollection.GetValue(StatTypes.Evade);
        healthRegen.text = "Health Regen: " + data.currentStatCollection.GetValue(StatTypes.HealthRegen);
        manaRegen.text = "Mana Regen: " + data.currentStatCollection.GetValue(StatTypes.ManaRegen);
        movementRange.text = "Movement: " + data.currentStatCollection.GetValue(StatTypes.MovementRange);



        Name.text = data.Name;
        level.text = "Level: " + data.Level;
        exp.text = "Experience: "+ data.XP +"";
        race.text = "Race: " + data.race;
        pjob.text = "Primary Job: "+ data.primaryJob;

        if (data.secondaryJob == "")
        {
            sjob.text = "Secondary Job: " + "---";

        }
        else
        {
            sjob.text = "Secondary Job: " + data.secondaryJob;
        }


        description.text = data.description;

        foreach (Tuple<string, string> item in data.Relationships.GetAllRelationshipScores())
        {
            TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, relContainer.contentTransform);
            relContainer.gos.Add(temp.gameObject);
            temp.text = (item.ele1 + ": " + item.ele2);
        }


        foreach (string tag in data.actorPropertyTags)
        {
            TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, tags.contentTransform);
            tags.gos.Add(temp.gameObject);
            temp.text = (tag);
        }

        DisplayItems();
        CurrentSkills();
    }






    void CurrentSkills()
    {


        foreach (Skill k in currActor.JobDataState.GetAllLearnedSkills(currActor.race,currActor.primaryJob, currActor.secondaryJob))
        {
            TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, skills.contentTransform);
            skills.gos.Add(temp.gameObject);
            temp.text = (k.skillName);

        }

        foreach (Buff k in currActor.buffContainer.buffList)
        {
            TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, buffs.contentTransform);
            buffs.gos.Add(temp.gameObject);
            temp.text = (k.buffName);

        }


    }

    public void EditEquipment()
    {
        equipPanel.PopulatePanel(currActor);
    }

    public void EditJobs()
    {
        talentPanel.PopulatePanel(currActor);
    }

    public void DisplayTalent()
    {

        foreach (string k in currActor.JobDataState.GetAllJobsUnlocked())
        {
          //TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, leftPanel.contentTransform);
          //leftPanel.gos.Add(temp.gameObject);
          //temp.text = (k);
            
        }


    }

    public void DisplayItems()
    {

        foreach (EquipmentSlottt equip in currActor.equipment.GetAllEquipement())
        {
            TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, equipment.contentTransform);
            equipment.gos.Add(temp.gameObject);
            temp.text = Globals.campaign.GetItemData(equip.itemKey).Name;
        }

        foreach (ItemContainer i in currActor.inventory.ItemSlots)
        {
            TMP_Text temp = Instantiate<TMP_Text>(buttonPrefab, inventory.contentTransform);
            inventory.gos.Add(temp.gameObject);
            temp.text = (Globals.campaign.GetItemData(i.itemKey).Name);
        }

    }

    public void AdjustContent()
    {
        equipment.AdjustContentLength();
        inventory.AdjustContentLength();
        skills.AdjustContentLength();
        buffs.AdjustContentLength();
    }
    public void Clearpanels()
    {
        equipment.CleanUp();
        inventory.CleanUp();
        skills.CleanUp();
        buffs.CleanUp();
    }
}


public abstract class CharacterInfoDisplayPanel : MonoBehaviour
{

    public abstract void PopulatePanel(ActorData data);


}


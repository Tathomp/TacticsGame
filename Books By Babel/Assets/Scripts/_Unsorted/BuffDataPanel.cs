using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffDataPanel : MonoBehaviour
{
    //Editor
    public TMP_InputField buffName, description;
    public TMP_InputField maxStacks, duration; //int only fields

    public ScrollListScaleableContent tagList, effectsList;
    //public TMP_InputField buffEffectPrefab; //we'll also need tag prefabs
    public Image image;
    public Toggle isBuff, isTrait, isTemp;

    //Prefabs
    public TextButton buffEffectButtPrefab;

    //Private
    private ButtonListContainer buffeffectsList = new ButtonListContainer();
    private ButtonListContainer tagContainer = new ButtonListContainer();


    private Buff currBuff;

    public EffectEditPanel editPanel;

    public void DisplayBuffData(Buff buff)
    {
        currBuff = buff;

        buffName.text = buff.buffName;
       //W description.text = buff.GetHotbarDescription();

        // maxStacks.text = buff.maxStacks + "";
        //   duration.text = buff.turnDuration + "";


        image.sprite = Globals.GetSprite(FilePath.IconSpriteAtlas, buff.iconKey);

        isBuff.isOn = buff.IsBuff;
        isTrait.isOn = buff.IsTrait;
        isTemp.isOn = buff.tempBuff;

        maxStacks.text = buff.maxStacks + "";
        duration.text = buff.turnDuration + "";


        PrintBuffEffects();
    }

    void PrintBuffEffects()
    {
        buffeffectsList.ClearList();

        foreach (BuffEffect effect in currBuff.effects)
        {
            TextButton b = Instantiate<TextButton>(buffEffectButtPrefab, effectsList.contentTransform);
            b.ChangeText(effect.PrintNameOfEffect());
            b.button.onClick.AddListener(delegate { editPanel.InitEffect(effect); });
            buffeffectsList.AddToList(b);
        }
    }

    void ClearBuffEffects()
    {
        buffeffectsList.ClearList();
    }
}

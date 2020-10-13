using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditEffectConditonals : MonoBehaviour
{
    //editor
    public EditRandomRollConditional randomEditPanel;
    public StatConditionalEditPanel statEditPanel;
    public EditTagConditionalPanel tagEditPanel;

    public ScrollListScaleableContent content;
    public TextButton buttonPrefab;

    ButtonListContainer conditionButtons = new ButtonListContainer();
    GameObject currGo;
    List<Conditional> conditionalList;
    public Conditional currConditional;

    public void AddCondintional(Conditional c)
    {
        conditionalList.Add(c);
        SpawnButton(c);

        conditionButtons.SelectLast();
    }

    void SpawnButton(Conditional conditional)
    {
        TextButton b = Instantiate<TextButton>(buttonPrefab, content.contentTransform);
        conditionButtons.AddToList(b);

        if (conditional is MatchingTagConditional)
        {
            b.ChangeText("Matching");
            b.button.onClick.AddListener(delegate { tagEditPanel.InitPanel(conditional); });
        }
        else if (conditional is RandomRollConditional)
        {
            b.ChangeText("Random");
            b.button.onClick.AddListener(delegate { randomEditPanel.Init(conditional); });


        }
        else if (conditional is StatThresholdConditional)
        {
            b.ChangeText("StatThreshold");
            b.button.onClick.AddListener(delegate { statEditPanel.InitPanel(conditional); });

        }
    }

    public void InitConditionalEdit(List<Conditional> c)
    {
        conditionalList = c;
        conditionButtons.ClearList();

        gameObject.SetActive(true);

        foreach (Conditional conditional in conditionalList)
        {
            SpawnButton(conditional);
        }

        conditionButtons.SelectFirst();
    }

    public void DeleteConditional()
    {
        conditionalList.Remove(currConditional);
    }


    public void ToggleOff()
    {
        gameObject.SetActive(false);
        conditionButtons.ClearList();
    }

    public void SwitchContexts(GameObject ob)
    {
        if(currGo != null)
        {
            currGo.SetActive(false);
        }

        ob.SetActive(true);
        currGo = ob;
    }
}

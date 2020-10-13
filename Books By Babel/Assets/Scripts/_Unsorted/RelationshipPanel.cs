using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipPanel : MonoBehaviour {

    // Prefabs
    public RelationshipButton relationshipButton;
    public Transform ButtonContainer;

    // Scene Regs
    public BaseManager baseManager;

    List<RelationshipButton> buttons;
    List<ActorData> actors;
    RelationshipMap Rmap;

    public void InitPanel(List<ActorData> actor, RelationshipMap map)
    {
        actors = actor;
        Rmap = map;

        buttons = new List<RelationshipButton>();

        gameObject.SetActive(true);

        PopulateList();

    }


    void PopulateList()
    {

        for (int i = actors.Count - 1; i >= 0; i--)
        {
            List<Tuple<string, int>> n = actors[i].Relationships.GetAllRelationships();

            foreach (Tuple<string, int> t in n)
            {
                if(actors[i].Relationships.HasRelationship(t.ele1))
                {
                    //Make the button here
                    CreateButton(actors[i].GetKey(), t.ele1, t.ele2);

                }
            }
        }
    }

    void CreateButton(string a1, string a2, int v)
    {
        string csKey = Rmap.GetCutSceneKey(a1, a2, v);
        CutScene cs = baseManager.campaign.GetCutsceneData(csKey);

        RelationshipButton b = Instantiate<RelationshipButton>(relationshipButton, ButtonContainer);
        //Button b = new RelationshipButton();
        b.InitDisplay(a1, a2, Rmap.GetRelationshipLevel(v));
        b.GetComponent<Button>().onClick.AddListener(delegate { CutsceneClicked(cs); });
        buttons.Add(b);

    }

    internal void ToggleOff()
    {
        baseManager.baseUI.cutsceneController.ToggleOff();
        gameObject.SetActive(false);
    }

    public void CutsceneClicked(CutScene cutscene)
    {
        //baseManager.inputFSM.SwitchState(new RelationshipSceneInputState(baseManager, cutscene, dialogPanel));
        // switch to the general cutscene state
        CutScene cs = cutscene.Copy() as CutScene;

        baseManager.inputFSM.SwitchState(
            new CutsceneInputState(baseManager,
            cs, baseManager.baseUI.cutsceneController,
            CinematicStatus.RelationshipScene)
            );
    }

    private void OnDisable()
    {
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Destroy(buttons[i]);
            GameObject.Destroy(buttons[i].gameObject);
        }

        buttons = new List<RelationshipButton>();
    }
}

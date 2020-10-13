using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour {

    public SkillPanel skillPanel;
    public AbilityInUseLabel SkillInUseName;
    public BoardManager boardManager;
    public TileSelectionPanel tileselctionPanel;
    public ActionMenu actionMenu;
    public TurnOrderPanel turnOrderPanel;
    //public GameObject characterSelectPanel;
    public ScrollListScaleableContent selectionList;
    public CharacterSelectButton characterSelectionPrefab;
   // public CharacterSelectInfoPanel characterSelectionInfoPanel;
    public TextButton placeUnitButotn;
    public PreviewManager previewmanager;
    public RewardPanel rewardPanel;
    public ToolbarPanel hotbarPanel;
    public OptionsMenu optionsMenu;
    public PartyEditPanel characterInfoPanel, inBattleCharacterInfoPanel;
    public MissionObjectivesPanel objectivesPanel;
    public DirectionSelector dirSelector;
    public ActorInfoPanel actorInfoPanel;
    public PublicEventPanel eventDisplayPanel;

    public DialogPanel dialogPanel;
    public DialogChoicePanel dialogChoice;

    public GameObject gameoverPanel;
    public CameraBackgroundContoller backgroundContoller;

    public CameraPositonController cameraPositonController;
    public SelectedCharacterPanel selectedCharacterPanel;

    List<CharacterSelectButton> buttons;

    public void Awake()
    {
        eventDisplayPanel.gameObject.SetActive(false);
        actionMenu.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
        rewardPanel.gameObject.SetActive(false);
        hotbarPanel.gameObject.SetActive(false);
        //turnOrderPanel.gameObject.SetActive(false);
        tileselctionPanel.gameObject.SetActive(false);

        dialogChoice.gameObject.SetActive(false);
        dialogPanel.gameObject.SetActive(false);

        optionsMenu.ToggleOff();
       // characterSelectionInfoPanel.gameObject.SetActive(false);
        characterInfoPanel.gameObject.SetActive(false);
        SkillInUseName.ToggleOff();
        objectivesPanel.ToggleOff();

        buttons = new List<CharacterSelectButton>();
    }

    public void ToggleOffCharacterInfoPanel()
    {
        //characterInfoPanel.gameObject.SetActive(false);
        boardManager.inputFSM.SwitchState(new UsersTurnState(boardManager));
    }

    

    public void CloseSelection()
    {
        foreach (CharacterSelectButton button in buttons)
        {
            button.button.onClick.RemoveAllListeners();
        }
    }

    


    public void PlaceUnitButton(ActorData actor)
    {
        //boardManager.inputFSM.SwitchState(new PlaceUnitState(boardManager, actor));
        placeUnitButotn.button.onClick.RemoveAllListeners();

    }


    public void DisplayPreviewPanel(List<CombatNode> nodes)
    {
        previewmanager.InitPreview(nodes);        

    }

    public void CleanUpPreview()
    {
        previewmanager.ClearPreview();
    }

    
    //god this is so bad
    public void RemoveSelection(ActorData data, Party party)
    {
        List<Actor> actors = boardManager.spawner.actors;

        Actor actor = boardManager.spawner.GetActor(data);

        data.selected = false;
        actor.DestoryActor();
        actors.Remove(actor);

        boardManager.spawner.ActorDataGameObjectMap.Remove(data);

       // boardManager.inputFSM.SwitchState(new PlaceUnitState(boardManager, null));
        boardManager.inputFSM.SwitchState(new CharacterPlacementState(boardManager));

        /*
        for (int i = 0; i < actors.Count; i++)
        {
            if(actors[i].actorData == data)
            {
                data.selected = false;
                actors[i].DestoryActor();
                actors.RemoveAt(i);
                PopulateSelection(party);

                boardManager.inputFSM.SwitchState(new PlaceUnitState(boardManager, null));
                boardManager.inputFSM.SwitchState(new CharacterPlacementState(boardManager));

                return;
                //maybe remove it from pathfinding?
            }
        }

        Debug.Log("IMPLEMENT PLS");
        */
    }


    public void ToggleOffBattleUI()
    {
        actionMenu.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
        turnOrderPanel.gameObject.SetActive(false);
        tileselctionPanel.gameObject.SetActive(false);
        objectivesPanel.gameObject.SetActive(false);
        hotbarPanel.gameObject.SetActive(false);
    }

    //This is basically so that tile data, and turn order don't display when we're selecting characters
    public void TurnOffInfoPanels()
    {
        turnOrderPanel.gameObject.SetActive(false);
        tileselctionPanel.gameObject.SetActive(false);
    }

    public void TurnInfoPanels()
    {
        turnOrderPanel.gameObject.SetActive(true);
        tileselctionPanel.gameObject.SetActive(true);
    }

    public void ToggleOnBattleUI()
    {
        actionMenu.InitMenu(boardManager);
        skillPanel.boardManager = boardManager;

        turnOrderPanel.gameObject.SetActive(true);
        tileselctionPanel.gameObject.SetActive(true);
        objectivesPanel.InitMissionObjectivePanel(boardManager.currentMission);
        hotbarPanel.gameObject.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I think this is going to be the controller for the cutscene scene
/// We'll load this from theaksfdjf
/// </summary>
public class CutsceneController : MonoBehaviour
{
    public bool test;

    //public GameObject parent;


    private CutScene cs;
    private CutSceneAction currentAction;
    public InputFSM stateMachine;

    private int mapsizeX, mapsizeY;

    //new stuff
    public GameObject[,] board, tileeffects;
    private List<GameObject> actors;
    [HideInInspector]
    public List<AnimationObject> skillObjects;

    

    //prefabs
    public GameObject tilespritePrefab;
    public GameObject actorAnimation;

    //editor
    public BGMController bgm;
    public SFXController sfx;
    public Transform spawnPoint;
    public Camera cutsceneCamera;


    //old stuff
    public DialogPanel dialogPanel;
    public DialogChoicePanel choicePanel;

    //mapping
    public Dictionary<string, GameObject> uidGameObjectMap;

    public void NewInput()
    {
        if (currentAction.IsChoiceAction())
        {
            stateMachine = new InputFSM(new CutsceneChoiceState(this, currentAction as ChoiceAction, CinematicStatus.RelationshipScene));
        }
        else if(currentAction.IsDialogueAction())
        {
            stateMachine = new InputFSM(new CutsceneDialoguInput(this, cs, currentAction));

        }
        else
        {
            //our default input state
            stateMachine = new InputFSM(new BlockUserInputState());
            StartCoroutine(currentAction.ExecuteAction(this));

        }
    }


    public void NextNode()
    {
        if (cs.IsEmpty() == false)
        {
            currentAction = cs.NextAction();
            NewInput();
        }
        else
        {
            //the cutscene is done
            //we should re enable all the stuff we disabled before
            //maybe send a signal that the cutscene was played or something so that it doesnt
            //keep playing
            Debug.Log("Attempting to delete game objects");

            Globals.cutsceneData.currentFIle.SwitchScene();

            //ToggleOff();
        }
    }

    private void Start()
    {
        // Toggle on to test, idk i kinda forgot why i did this //
        //                                                      //

        if (test)
        {
            GenerateDemoCampaign c = new GenerateDemoCampaign();

            cs = c.campaign.GetCutsceneCopy("test_cutscene");
            Globals.campaign = c.campaign;
        }
        else
        {
            cs = Globals.cutsceneData.currentScene;
        }
        
        Globals.campaign.AddCutsceneToWatchList(cs);

        InitCutscene(cs);



    }


    public void Update()
    {
        stateMachine.ProcessInput();
    }

    public void InitCutscene(CutScene cs)
    {
        this.cs = cs;
        uidGameObjectMap = new Dictionary<string, GameObject>();
        skillObjects = new List<AnimationObject>();

        ToggleOn();

        PrintBoard();
        DisplayActors();

        currentAction = cs.NextAction();
        NewInput();
    }

    public void PrintBoard()
    {
        MapDataModel model = Globals.campaign.GetMapDataContainer().mapDB.GetData(cs.mapname);
        
        board = new GameObject[model.sizeX, model.sizeY];
        tileeffects = new GameObject[model.sizeX, model.sizeY];


        mapsizeX = model.sizeX;
        mapsizeY = model.sizeY;


        for (int x = 0; x < model.sizeX; x++)
        {
            for (int y = 0; y < model.sizeY; y++)
            {
                GameObject tempTile = Instantiate<GameObject>(tilespritePrefab, spawnPoint);
                board[x, y] = tempTile;
                tempTile.transform.position = Globals.GridToWorld(x, y);
                // change the tile sprite
                tempTile.GetComponent<SpriteRenderer>().sprite = Globals.GetSprite(FilePath.TileSetAtlas, model.tileBoard[x, y]);
            
            }
        }

    }
    

    public void DisplayActors()
    {
        actors = new List<GameObject>();

        foreach (string key in cs.actorIDMap.Keys)
        {
            CutsceneActorPositionData data = cs.actorIDMap[key];

            GameObject tempActor = Instantiate<GameObject>(actorAnimation, spawnPoint);
            actors.Add(tempActor);
            tempActor.transform.position = Globals.GridToWorld(data.position.X, data.position.Y);
            string animID = Globals.campaign.contentLibrary.actorDB.GetData(data.actorID).animationController;
            Globals.AddAnimationController(animID, tempActor);


            uidGameObjectMap.Add(key, tempActor);
        }
    }


    public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        DeleteAll();



        //gameObject.SetActive(false);
    }

    public void DeleteAll()
    {

        //delete board, tile effects
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                if(board[x,y] != null)
                {
                    Destroy(board[x, y].gameObject);
                    board[x, y] = null;
                }

                if(tileeffects[x,y] != null)
                {
                    Destroy(tileeffects[x, y].gameObject);
                    tileeffects[x, y] = null;
                }

            }
        }


        //delete actor
        int count = actors.Count - 1;
        for (int i = count; i >= 0; i--)
        {
            Destroy(actors[i].gameObject);

        }

        //delete skill
        count = skillObjects.Count - 1;
        for (int i = count; i >= 0; i--)
        {
            skillObjects.RemoveAt(i);
        }

    }
}

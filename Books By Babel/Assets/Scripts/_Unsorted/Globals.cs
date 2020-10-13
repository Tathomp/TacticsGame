using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public static class Globals
{
    public static float WidthHalf = .5f, HeightHalf = .25f;
    public static float TileSelectionLayer = .5f, selectorLayer = .5f, playerLayer = .75f, objLayer = .5f;
    public static float heightOffset = .25f;


    public static Campaign campaign;
    public static ContentLibrary currentLibrary;
    public static CutsceneData cutsceneData;
    
    public static GameState currState;

    public static int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }


    public static void PlaySfx(string key)
    {
        SFXController.sfxInstance.ChangeSong(key);
    }


    public static AnimationObject GenerateAnimationObject(string id, TileNode source, TileNode dest)
    {
        return GenerateAnimationObject(id, new MapCoords(source.data.posX, source.data.posY), new MapCoords(dest.data.posX, dest.data.posY));
    }


    public static ColorDatabase GetColorDatabase()
    {
        return Resources.Load<ColorDatabase>("Color/Glossary");
    }


    public static ColorDatabaseEntry GetColorDatabaseEntry(string k)
    {
        return GetColorDatabase().GetEntry(k);
    }


    public static AnimationObject GenerateAnimationObject(string id, MapCoords source, MapCoords dest)
    {
        AnimationObject temp = Resources.Load<AnimationObject>("SkillAnimationControllers/" + id);

        MapCoords position;
        if(temp.startAtSource)
        {
            position = source;
        }
        else
        {
            position = dest;
        }

        AnimationObject obj = GameObject.Instantiate<AnimationObject>(temp, Globals.GridToWorld(position.X, position.Y), Quaternion.identity);
        /*
        Debug.Log("attempt to load: " + "AnimationControllers/" + id);
        obj.GetComponent<Animator>().runtimeAnimatorController = 
            Resources.Load<RuntimeAnimatorController>("AnimationControllers/" + id);
        */

        return obj;
    }
    

    public static TileEffectSprite GenerateTileEffectSprite(int x, int y, string animationID)
    {
        TileEffectSprite s = GameObject.Instantiate<TileEffectSprite>(
           Resources.Load<TileEffectSprite>("BaseObjects/TileEffect"),
           Globals.GridToWorld(x, y),
           Quaternion.identity);

        s.GetComponent<Animator>().runtimeAnimatorController =
            Globals.GEtAnatimationController(animationID);



        return s;
    }


    public static string GenerateRandomHex()
    {
        string hexString = "";

        for (int i = 0; i < 16; i++)
        {
            int num = Random.Range(0, 16);
            hexString += num.ToString("X");

        }


        return hexString;
    }


    public static Vector3 GridToWorld(TileNode node)
    {
        return GridToWorld(node.data.posX, node.data.posY);
    }


    public static Vector3 GridToWorld(int x, int y)
    {
        return new Vector3(x, y, 0);
    }
    


    public static Vector2 WorldToGrid(string worldPos)
    {
        Vector2 gridPos = new Vector2();
        string[] coords = worldPos.Split(' ');

        coords[1] = coords[1].Replace("(Clone)", "");


        gridPos.x = int.Parse(coords[0]);
        gridPos.y = int.Parse(coords[1]);


        return gridPos;
    }


    public static Vector2 MouseToWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

        if (hit)
        {
            Vector2 grid = Globals.WorldToGrid(hit.collider.name);

            int x = (int)grid.x;
            int y = (int)grid.y;

            return new Vector2(x, y);
        }

        return Vector2.negativeInfinity;
    }


    public static void AssignTileSprite(GameObject go, string filepath)
    {
        go.GetComponent<SpriteRenderer>().sprite = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas).GetSprite(filepath);
    }

    public static Sprite GetPortrait(string fp)
    {
        return GetSprite(FilePath.ActorSpriteAtlas, fp);
    }

    public static Sprite GetSprite(string fp)
    {
        return Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas).GetSprite(fp);
    }

    public static Sprite GetSprite(string atlasName, string spriteName)
    {
        return Resources.Load<SpriteAtlas>(atlasName).GetSprite(spriteName);
    }

    public static BoardManager GetBoardManager()
    {
        return GameObject.Find("BoardManager").GetComponent<BoardManager>();
    }


    public static PropertyTagMap<int, TileEffect> GetPorpertyMap()
    {
        return campaign.GetPropertyMaps().tileEffectMap;
    }
    public static void SpawnMonster(string id, MapCoords pos)
    {
        if(GetBoardManager().pathfinding.HasActor(pos.X, pos.Y) == false)
        {
            ActorData temp = campaign.contentLibrary.actorDB.GetCopy(id);
            temp.gridPosX = pos.X;
            temp.gridPosY = pos.Y;

            SpawnMonster(temp);
        }
    }

    public static void SpawnMonster(ActorData data)
    {
        BoardManager bm = GetBoardManager();
        bm.currentMission.npcs.Add(data);
        bm.spawner.GenerateActor(data, bm);
    }

    public static void SpawnMonster(string id)
    {
        SpawnMonster(
            campaign.contentLibrary.actorDB.GetCopy(id)
            );
    }

    public static void CleanButtons(List<Button> buttons)
    {
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            buttons[i].onClick.RemoveAllListeners();
            GameObject.Destroy(buttons[i]);
            GameObject.Destroy(buttons[i].gameObject);

        }

        buttons = new List<Button>();
    }


    public static List<string> ParseFileNames(string dir, string extFilter)
    {
        List<string> files = new List<string>();


        foreach (string item in Directory.GetFiles(dir))
        {
            int i = item.LastIndexOf('/') + 1;
            string s = item.Substring(i);
            if(s.EndsWith(extFilter))
            {
                files.Add(s);
            }
        }

        return files;
    }

    public static void AddAnimationController(string controllerName, GameObject go)
    {
        go.GetComponent<Animator>().runtimeAnimatorController = GEtAnatimationController(controllerName);
    }

    public static RuntimeAnimatorController GEtAnatimationController(string controllerName)
    {
        return Resources.Load<RuntimeAnimatorController>("AnimationControllers/" + controllerName);
    }
}

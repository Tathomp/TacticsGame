using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavedFileMission : SavedFile
{
    public Mission currentMission;
    /// Map State data
    /// 
    public List<TileEffect>[,] TileEffectStates;
    public TileTypes[,] TileTypeStates;

    public int currentTurnSpeed;

    public SavedFileMission( Campaign campaign, Mission currentMission) 
        : base (campaign)
    {
        this.currentMission = currentMission;

        currentTurnSpeed = 0;
    }

    public void SaveTileState(TileNode[,] nodes)
    {
        int lengthx = nodes.GetLength(0);
        int lengthy = nodes.GetLength(1);

        TileEffectStates = new List<TileEffect>[lengthx, lengthy];
        TileTypeStates = new TileTypes[lengthx, lengthy];

        for (int x = 0; x < lengthx; x++)
        {
            for (int y = 0; y < lengthy; y++)
            {
                TileEffectStates[x, y] = new List<TileEffect>();

                foreach (TileEffect effect in nodes[x,y].tileEffects)
                {
                    if(effect is AuraTileEffect)
                    {
                        TileEffectStates[x, y].Add(effect.Copy() as AuraTileEffect);

                    }
                    else
                    {
                        TileEffectStates[x, y].Add(effect.Copy() as TileEffect);
                    }
                }

                TileTypeStates[x, y] = nodes[x, y].type;
            }
        }
    }

    public override void SwitchScene()
    {
        //switch to the battle scene
        SceneManager.LoadScene("BoardScene");
    }
}

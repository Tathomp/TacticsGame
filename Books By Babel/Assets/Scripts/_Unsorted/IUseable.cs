using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable : IHotbar
{
    string GetKey();
    int GetMaxRange(Actor data);
    int GetMinRange(Actor data);
    int GetCoolDown();
    //ITargetable GetTargetType();

    void ProcessTags(Actor source, List<TileNode> center);
    List<TileNode> GetTargetedTiles(Actor source, TileNode center);
    List<TileNode> GetFinalTargetedTiles(Actor source, TileNode center);
    void ProcessEffects(Combat c, Actor source, TileNode tile);

    List<string> GetTags();

    string GetName();
    string GetSFXKey();
    List<string> GetAnimControllerID();
    bool FilterTileNode(Actor source, TileNode center);

    TargetFiltering GetTargetFiltering();
    ITargetable GetTargetType();

    void PayCosts(Actor actor);
    bool CanPayCost(Actor actor);

}

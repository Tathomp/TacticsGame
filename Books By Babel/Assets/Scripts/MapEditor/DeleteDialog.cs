using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeleteDialog : MonoBehaviour {

    public Text mapNames;
    public InputField input;
    SavedDatabase<MapDataModel> db;

    // Use this for initialization
    void Start () {
        PopulateMapNames();
	}
	
	public void PopulateMapNames()
    {
        mapNames.text = "";

        db = Globals.campaign.GetMapDataContainer().mapDB;

        string[] d = db.DbKeys();

        for (int i = 0; i < d.Length; i++)
        {
            mapNames.text += d[i] + "\n";
        }
    }

    public void DeleteMap()
    {
        db.RemoveEntry(input.text);
        PopulateMapNames();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

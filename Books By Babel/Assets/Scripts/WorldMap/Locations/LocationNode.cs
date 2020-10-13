using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationNode
{
    public MapCoords coords;
    public string AreaName { get; set; }
    public string MapKey { get; set; }
    public string FlavorText { get; set; }

    public List<MapCoords> neighbors;
    public List<LocationComponent> locationcomponents;

    public string filepath;



    public LocationNode(string areaname, string mapkey, string filepath, int x, int y )
    {
        AreaName = areaname;
        MapKey = mapkey;
        this.filepath = filepath;
        coords = new MapCoords(x, y);

        neighbors = new List<MapCoords>();
        locationcomponents = new List<LocationComponent>();
    }

    public static void AddNeighbors(LocationNode node1, LocationNode node2)
    {
        node1.neighbors.Add(node2.coords);
        node2.neighbors.Add(node1.coords);
    }
}

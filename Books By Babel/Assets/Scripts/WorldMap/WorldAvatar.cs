using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAvatar : MonoBehaviour {

    public MapCoords position;


	public void AssignSprite(string filepath)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Globals.GetSprite(FilePath.ActorSpriteAtlas, filepath);
    }

    public void StepTowards(int x, int y)
    {
        position.X += x;
        position.Y += y;

        MoveTo(position.X, position.Y);
    }

    public void MoveTo(int x, int y)
    {
        position.X = x;
        position.Y = y;

        UpdateWorldMapPositionData();

        StartCoroutine(MovementAnimation());
    }

    public void SetPosition(int x, int y)
    {
        position.X = x;
        position.Y = y;

        UpdateWorldMapPositionData();

        this.transform.position = Globals.GridToWorld(x, y);
    }

    void UpdateWorldMapPositionData()
    {
        Globals.campaign.worldMapDictionary[Globals.campaign.currentWorldMap].ChangeCurrentPos(position.X, position.Y);
    }

    public IEnumerator MovementAnimation()
    {
        Vector3 target = Globals.GridToWorld(position.X, position.Y);
        float remainingDist = (transform.position - target).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {

            transform.position = Vector3.MoveTowards(transform.position, target, 5 * Time.deltaTime);
            remainingDist = (transform.position - target).sqrMagnitude;
            yield return null;
        }

        // THis is where we can block input or changed
        Debug.Log("Reacehd destination");
    }

    public void MoveAlongPath(List<LocationNode> path)
    {
        StartCoroutine(ProcessPath(path));        
    }

    IEnumerator ProcessPath(List<LocationNode> path)
    {

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 target = Globals.GridToWorld(path[i].coords.X, path[i].coords.Y);

            float remainingDist = (transform.position - target).sqrMagnitude;

            while (remainingDist > float.Epsilon)
            {

                transform.position = Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
                remainingDist = (transform.position - target).sqrMagnitude;
                yield return null;
            }
        }
    }

    public IEnumerator SmoothMovement(Vector3 target)
    {
        float remainingDist = (transform.position - target).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {

            transform.position = Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
            remainingDist = (transform.position - target).sqrMagnitude;
            yield return null;
        }

    }
}

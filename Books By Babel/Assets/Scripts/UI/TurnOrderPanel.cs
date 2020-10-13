using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOrderPanel : MonoBehaviour {

    public TurnOrderIcon buttonPrefab;
    public Transform container;
    public TMP_Text turnorder_header;

    List<TurnOrderIcon> turnOrderButtons;

    public int maxDisplay = 5;

    public void Awake()
    {
        turnOrderButtons = new List<TurnOrderIcon>();
    }

    private void OnDisable()
    {
        //CleanButtons();
    }

    public void PopulateTurnOrder(List<Actor> actors, Selector selector)
    {
        List<ActorData> a = new List<ActorData>();

        foreach (Actor actor in actors)
        {
            a.Add(actor.actorData);
        }

        PopulateTurnOrder(a, selector);

        gameObject.SetActive(true);
    }

    public void PopulateTurnOrder(List<ActorData> actors, Selector selector)
    {
        CleanButtons();

        int i = 0;

        foreach (ActorData a in actors)
        {
            if (a.isAlive)
            {
                TurnOrderIcon temp = Instantiate<TurnOrderIcon>(buttonPrefab, container);
                temp.InitIcon(Globals.GetBoardManager().spawner.GetActor(a));
                temp.buttonComponet.onClick.AddListener(delegate { TurnOrderClick(a, selector); });
                turnOrderButtons.Add(temp);
                i++;

                if (i == maxDisplay)
                {
                    return;
                }
            }
        }
    }

    public void TurnOrderClick(ActorData actor, Selector selector)
    {
        selector.MoveTo(actor.gridPosX, actor.gridPosY);
    }

    void CleanButtons()
    {
        int count = turnOrderButtons.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            turnOrderButtons[i].buttonComponet.onClick.RemoveAllListeners();
            Destroy(turnOrderButtons[i].gameObject);
            Destroy(turnOrderButtons[i]);
        }

        turnOrderButtons = new List<TurnOrderIcon>();
    }

    public void UpdateTurnOrderHeaderDisplay(int x)
    {
        turnorder_header.text = "Turn: " + x;
    }
}

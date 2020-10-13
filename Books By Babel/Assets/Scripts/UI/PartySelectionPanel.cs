using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectionPanel : MonoBehaviour {

    public CharacterEditorPanel characterEditor;

    public Button CharacterButton;

    public List<Button> characterButotns = new List<Button>();


    public void OnDisable()
    {
        ClearButtons();
        characterEditor.gameObject.SetActive(false);
    }

    public void ToggleOn(List<ActorData> actors)
    {
        characterButotns = new List<Button>();

        gameObject.SetActive( true);
        InitButtonList(actors);
    }


    void InitButtonList(List<ActorData> actors)
    {
        foreach (ActorData actor in actors)
        {
            Button temp = Instantiate<Button>(CharacterButton, this.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = actor.Name;
            temp.onClick.AddListener(delegate { CharacterClicked(actor); });
            characterButotns.Add(temp);
        }       

    }


    public void CharacterClicked(ActorData actor)
    {
        characterEditor.gameObject.SetActive(false);
        Debug.Log(actor.Name);
       //SS characterEditor.ToggleOn(actor);
    }


    void ClearButtons()
    {
        for (int  i = characterButotns.Count - 1;  i >= 0;  i--)
        {
            characterButotns[i].onClick.RemoveAllListeners();
            Destroy(characterButotns[i].gameObject);
            Destroy(characterButotns[i]);

        }
    }


}

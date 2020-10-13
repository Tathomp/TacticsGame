using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour {

    public CutsceneController controller;
    public TMP_Text dialogText;
    public TMP_Text speakerTitle;
    public Image characterportriat;

    Coroutine coroutine;
    string curText;


    public void UpdateDialog(string s, string speaker, Sprite cp)
    {

        gameObject.SetActive(true);

        curText = s;
        speakerTitle.text = speaker;
        // dialogText.text = s;
        characterportriat.sprite = cp;

        coroutine = StartCoroutine(TypeWriterEffect(s));
        
    }

    public void UpdateDialog(string s, string speaker, string actorKey)
    {
        string p = Globals.campaign.contentLibrary.actorDB.GetCopy(actorKey).portraitFilePath;

        UpdateDialog(s, speaker, Globals.GetSprite(FilePath.ActorSpriteAtlas, p));
    }

    public void UpdateDialog(DialogueAction action)
    {
        UpdateDialog(action.dialog,
                     Globals.campaign.contentLibrary.actorDB.GetData(action.actorID).Name,
                     action.actorID);

        gameObject.SetActive(true);
    }

    public void EnterPressed()
    {
        if(coroutine == null)
        {
            //go to next node
            controller.NextNode();
        }
        else
        {
            //stop coroutin
            StopCoroutine(coroutine);
            coroutine = null;

            dialogText.text = curText;
        }
    }
    
    IEnumerator TypeWriterEffect(string text)
    {
        dialogText.text = "";

        for (int i = 0; i <= text.Length; i++)
        {

            dialogText.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        coroutine = null;

    }
}

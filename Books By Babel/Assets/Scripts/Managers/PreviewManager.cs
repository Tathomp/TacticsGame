using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewManager : MonoBehaviour {

    public PreviewUIPanel prevPrefab;

    List<PreviewUIPanel> previews;
    int currentIndex, maxIndex;

    public void Start()
    {
        previews = new List<PreviewUIPanel>();

    }

    public void InitPreview(List<CombatNode> nodes)
    {
        maxIndex = 0;
        currentIndex = 0;

        if(nodes == null)
        {
            return;
        }

       
        foreach (CombatNode n in nodes)
        {
            
            PreviewUIPanel p = Instantiate<PreviewUIPanel>(prevPrefab, this.transform);
            previews.Add(p);
            p.InitText(n);
            p.gameObject.SetActive(false);

            maxIndex++;
            
        }

        maxIndex--;

        if(previews.Count == 0)
        {
            return;
        }


        for (int i = 0; i <= maxIndex; i++)
        {
            previews[i].UpdateIndex(i + 1, maxIndex + 1);
        }

        DisplayCurrentPreview();
    }

    public void ClearPreview()
    {
        if(previews == null)
        {
            return;
        }

        if(previews.Count == 0 )
        {
            return;
        }

        int c = previews.Count - 1;

        for (int i = c; i >= 0; i--)
        {
            GameObject.Destroy(previews[i].gameObject);
            GameObject.Destroy(previews[i]);
        }

        previews = new List<PreviewUIPanel>();
    }


    public void ShiftUp()
    {
        ToggleOffCurrent();

        if(previews.Count == 0)
        {
            return;
        }

        currentIndex++;

        if(currentIndex > maxIndex)
        {
            currentIndex = 0;
        }

        DisplayCurrentPreview();
    }

    public void ShfitDown()
    {
        ToggleOffCurrent();

        if (previews.Count == 0)
        {
            return;
        }

        currentIndex--;

        if(currentIndex < 0)
        {
            currentIndex = maxIndex;
        }

        DisplayCurrentPreview();
    }



    void DisplayCurrentPreview()
    {
        previews[currentIndex].gameObject.SetActive(true);
    }

    void ToggleOffCurrent()
    {
        previews[currentIndex].gameObject.SetActive(false);
    }

}

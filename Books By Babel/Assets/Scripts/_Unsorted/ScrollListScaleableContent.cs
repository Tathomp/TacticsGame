using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollListScaleableContent : MonoBehaviour
{
    public RectTransform contentTransform;
    public VerticalLayoutGroup group;
    public ButtonListContainer buttonConatiner = new ButtonListContainer();
    public List<GameObject> gos = new List<GameObject>();
    public int padding = 16;

    private void Start()
    {
        AdjustContentLength();
    }


    public TextButton InstantiatePRefab(TextButton b)
    {
        return Instantiate<TextButton>(b, contentTransform);
    }

    public void AdjustContentLength()
    {
        int value = padding;



        for (int i = 0; i < contentTransform.childCount; i++)
        {
          value += Mathf.RoundToInt(   contentTransform.GetChild(i).transform.gameObject.GetComponent<RectTransform>().sizeDelta.y + group.spacing);
        }



        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, value);
    }

    public void AddToList(TextButton butt)
    {
        buttonConatiner.AddToList(butt);
        AdjustContentLength();
    }

    public void CleanUp()
    {
        buttonConatiner.ClearList();

        int c = gos.Count -1;

        for (int i = c ; i >= 0; i--)
        {
            Destroy(gos[i]);
        }

        gos = new List<GameObject>();
    }

    private void OnDisable()
    {
        CleanUp();
    }
}

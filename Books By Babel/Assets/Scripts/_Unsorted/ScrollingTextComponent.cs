using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollingTextComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool useOffset = false;

    public TextMeshProUGUI text;
    public RectTransform rect, maskRect;
    public Transform textTransform;
    Coroutine c;
    float scrollSpeed = 25, maskWidth;


    float goalWidth;
    Vector3 startPos, target;
    // Start is called before the first frame update
    void Start()
    {
        rect = text.gameObject.GetComponent<RectTransform>();

        target = textTransform.localPosition;
        startPos = textTransform.localPosition;



        goalWidth = text.preferredWidth;
        maskWidth = maskRect.rect.width;
        target.x = target.x - goalWidth + maskWidth;

    }


    public void End()
    {
        StopCoroutine(c);
        rect.localPosition = startPos;
    }



    IEnumerator StartScroll()
    {
        float remainingDist = (rect.position - target).sqrMagnitude;

        while(remainingDist > .00005)
        //while (remainingDist > float.Epsilon)
        {
            Vector3 curr = new Vector3(textTransform.position.x, 0);
            textTransform.localPosition = Vector3.MoveTowards(textTransform.localPosition, target, scrollSpeed * Time.deltaTime);
           // rect.position = new Vector3(rect.position.x, yCorrd);
            remainingDist = (rect.position - target).sqrMagnitude;
          //  rect.position = new Vector3(rect.position.x, 0);

            yield return null;
        }

        //a wait here?
        //maybe we dont reset until hover off?


       // End();


    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        target = textTransform.localPosition;
        startPos = textTransform.localPosition;
        float xoffset = 0;

        if(useOffset)
        xoffset = (-target.x); //offset for when the mask and text aren't starting at the same relative pivot point i guess?


        goalWidth = text.preferredWidth;
        maskWidth = maskRect.rect.width;
        target.x = target.x - goalWidth + maskWidth;



        if((rect.rect.width + xoffset) > maskRect.rect.width) // this check assumes the same starting point?
        c = StartCoroutine(StartScroll());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(c != null)
        End();
    }
}

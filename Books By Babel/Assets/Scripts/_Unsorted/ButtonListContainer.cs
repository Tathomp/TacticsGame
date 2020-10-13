using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListContainer 
{
    List<TextButton> list = new List<TextButton>();

    public ButtonListContainer()
    {
      list  = new List<TextButton>();
    }

    public void SelectFirst()
    {
            ClickSelectedButton(0);
    }

    public void SelectLast()
    {
        if(list.Count > 0)
        SelectButton(list.Count - 1);

    }

    public void ClickSelectedButton(int i)
    {
        list[i].button.Select();
        list[i].button.onClick.Invoke();
    }

    public void SelectButton(int i)
    {
        list[i].button.Select();
    }

    public void AddToList(TextButton button)
    {
        list.Add(button);
    }

    public bool HasButtons()
    {
        return list.Count > 0;
    }

    public void ClearList()
    {
        int x = list.Count - 1;

        for (int i = x; i >= 0; i--)
        {
            list[i].button.onClick.RemoveAllListeners();
            GameObject.Destroy(list[i].gameObject);
            GameObject.Destroy(list[i]);
        }

        /*

         */

        list = new List<TextButton>();
    }
}

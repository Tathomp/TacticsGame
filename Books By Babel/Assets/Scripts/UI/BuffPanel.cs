using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanel : MonoBehaviour {

    public Transform container;
    public TMP_Text hover_display;
    public BuffIconDisplay BuffIconPrefab;
    public GameObject panel;
    public GameObject auraVisual;

    List<BuffIconDisplay> buffIcons;

    public void PopulateBar(List<Buff> data)
    {
        ClearList();
        if (data.Count > 0)
        {
            gameObject.SetActive(true);
            foreach (Buff buff in data)
            {
                InstantiateIcon(buff);
            }
        }
    }

    private void InstantiateIcon(Buff buff)
    {
        BuffIconDisplay i = Instantiate<BuffIconDisplay>(BuffIconPrefab, container);
        i.InitIcon(buff, hover_display, panel, auraVisual);
        buffIcons.Add(i);
    }


    public void ClearList()
    {
        if(buffIcons == null)
        {
            buffIcons = new List<BuffIconDisplay>();
        }

        for (int i = buffIcons.Count - 1; i >= 0; i--)
        {
            Destroy(buffIcons[i]);
            Destroy(buffIcons[i].gameObject);
        }

        buffIcons = new List<BuffIconDisplay>();
    }


    private void OnDisable()
    {
        ClearList();
       // hover_display.gameObject.SetActive(false);
    }
}

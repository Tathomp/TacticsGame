using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveListButton : MonoBehaviour
{
    public GameObject go;

    public void ToggleList()
    {
        go.SetActive(!go.activeSelf);
    }
}

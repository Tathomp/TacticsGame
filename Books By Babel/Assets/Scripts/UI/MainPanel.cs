using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    // editor
    public Button loadbutton;
    public Button savebutton;

	public void InitPanel()
    {
        loadbutton.gameObject.SetActive(true);
        savebutton.gameObject.SetActive(true);
        this.gameObject.SetActive(true);

    }

    public void DisaplyPanel()
    {
        loadbutton.gameObject.SetActive(false);
        savebutton.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

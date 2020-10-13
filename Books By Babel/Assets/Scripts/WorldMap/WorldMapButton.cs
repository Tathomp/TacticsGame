using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapButton : MonoBehaviour {

	public void ButtonClicked()
    {
        SceneManager.LoadScene("WorldMapScene");
    }
}

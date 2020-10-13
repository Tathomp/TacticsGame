using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void QuitToMenu()
    {
        CustomeSceneLoader.MainMenu();
    }

    public void ReloadMission()
    {
        CustomeSceneLoader.LoadBoardScene();
    }
}

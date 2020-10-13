using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomeSceneLoader : SceneManager
{
    public static void LoadCutsceneScene()
    {
        LoadScene("CutsceneScene");

    }

    public static void LoadBoardScene()
    {
        LoadScene("BoardScene");

    }

    public static void MainMenu()
    {
        LoadScene("MainMenu");

    }

    public static void CreationSquite()
    {
        LoadScene("CreationSuiteScene");

    }

    public static void LoadWorldMap()
    {
        LoadScene("WorldMapScene");

    }
}

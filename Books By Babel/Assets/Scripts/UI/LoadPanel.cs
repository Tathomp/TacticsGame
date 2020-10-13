using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour {

    // Editor
    public CreationSuiteManager creationManager;
    public Transform buttonContainer;
    public Button buttonprefab;

    // private
    List<Button> loadbuttons;
    string currentSelectedFile;

    public void InitLoadPanel()
    {
        gameObject.SetActive(true);

        loadbuttons = new List<Button>();

        List<string> files = Globals.ParseFileNames(FilePath.LibraryFolder, FilePath.ContLibExt);

        foreach (string s in files)
        {
            Button b = Instantiate<Button>(buttonprefab, buttonContainer);
            b.onClick.AddListener(delegate { FileButtonClicked(s); });
            b.GetComponentInChildren<Text>().text = s;
            loadbuttons.Add(b);
        }

    }

    private void OnDisable()
    {
        Globals.CleanButtons(loadbuttons);
    }

    private void FileButtonClicked(string s)
    {
        Debug.Log("File: " + s);
        currentSelectedFile = s;
    }

    public void LoadContentLibrary()
    {
        //creationManager.CurrentContentLibrary = SaveLoadManager.LoadFile(
        //    FilePath.LibraryFolder + currentSelectedFile) as ContentLibrary;

        ExitPanel();
    }

    public void ExitPanel()
    {
        gameObject.SetActive(false);
    }
}

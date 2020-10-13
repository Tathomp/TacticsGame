using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotkeyMenu : MonoBehaviour {

    public BoardManager board;
    public ScrollListScaleableContent buttonContainer;
    public TextButton hotkeyButtonPrefab;
    public TMP_Text textPrefab;

    public HotKeys hotkeys;

    List<TMP_Text> textlabels;
    List<TextButton> buttonlabels;

    public void InitList(HotKeys hotkeys)
    {
        ToggleOn();


        KeyBindingHelper helper = new KeyBindingHelper();
        textlabels = new List<TMP_Text>();
        buttonlabels = new List<TextButton>();

        KeyBindingNames[] names = hotkeys.GetKeys();

        foreach (KeyBindingNames n in names)
        {
            InitLabel(helper.GetBindingNames(n));
            InitButton(n, helper.GetKEyName(hotkeys.hotkeys[n]));
        }
      
    }

	public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ClearMenu();
    }

    public void ClearMenu()
    {
        
        for (int i = textlabels.Count - 1; i >= 0; i--)
        {
            Destroy(textlabels[i].gameObject);
            Destroy(textlabels[i]);
        }

        for (int i = buttonlabels.Count - 1; i >= 0; i--)
        {
            buttonlabels[i].button.onClick.RemoveAllListeners();
            Destroy(buttonlabels[i].gameObject);
            Destroy(buttonlabels[i]);
        }
    }


    void InitButton(KeyBindingNames binding, string s)
    {
        TextButton temp = Instantiate<TextButton>(hotkeyButtonPrefab, buttonContainer.contentTransform);
        temp.ChangeText( s);
        temp.button.onClick.AddListener(delegate { RebindDelagate(binding); });
        buttonlabels.Add(temp);
    }

    void InitLabel(string name)
    {
        TMP_Text temp = Instantiate<TMP_Text>(textPrefab, buttonContainer.contentTransform);
        temp.text = name;
        textlabels.Add(temp);
    }

    void RebindDelagate(KeyBindingNames binding)
    {
        board.inputFSM.SwitchState(new RebindState(board, binding));
    }
}

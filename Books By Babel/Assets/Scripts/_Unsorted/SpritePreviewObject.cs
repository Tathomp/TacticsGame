using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritePreviewObject : MonoBehaviour
{
    public Image sprite;

    private string tile_filepath;
    private EditTileTypeSprite panel;

    public void InitObject(string fp, EditTileTypeSprite editspritepanel)
    {
        tile_filepath = fp;
        Debug.Log(fp);

        sprite.sprite = Globals.GetSprite(fp);
        panel = editspritepanel;
    }

    public void ButtonClicked()
    {
        panel.UpdateCurrentSpritePreview(tile_filepath);

        panel.CloseGrid();
    }
}


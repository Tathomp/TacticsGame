using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class EditTileTypeSprite : MonoBehaviour
{
    //Editor
    public SpritePreviewObject sprie_preview_prefab;
    public EditTileTypesPanel panel;
    public Image preview;
    public GameObject sprite_grid;
    public ScrollListScaleableContent container;

    private List<SpritePreviewObject> sprite_list = new List<SpritePreviewObject>();
    private string current_sprite_fp;

    public void InitDisplay(EditTileTypesPanel panel)
    {
        this.panel = panel;

        UpdateCurrentSpritePreview(panel.GetCurrentTileType().spriteFilePath);
    }

    //Butt we presss
    public void ChangeSpriteButton()
    {
        sprite_grid.SetActive(true);
        
        ListAllSpires();

        //turn on tile grid
    }

    public void ListAllSpires()
    {

        SpriteAtlas atlas = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas);

        int count = atlas.spriteCount;
        Sprite[] s = new Sprite[count];

        atlas.GetSprites(s);

        foreach (Sprite item in s)
        {
            SpritePreviewObject obj = Instantiate<SpritePreviewObject>(sprie_preview_prefab, container.contentTransform);
            
            obj.InitObject(item.name.Remove(item.name.Length - 7), this);
            sprite_list.Add(obj);
        }
    }

    public void ChangePreview()
    {
        preview.sprite = Globals.GetSprite(current_sprite_fp);

    }

    public void UpdateCurrentSpritePreview(string fp)
    {
        current_sprite_fp = fp;

        ChangePreview();
    }

    public void SaveSprite()
    {
        panel.GetCurrentTileType().spriteFilePath = current_sprite_fp;
    }

    public void CloseGrid()
    {
        sprite_grid.SetActive(false);

        //clear buttons;
    }
}

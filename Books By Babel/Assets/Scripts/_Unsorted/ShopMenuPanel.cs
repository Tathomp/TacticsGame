using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuPanel : MonoBehaviour
{
    private Shop currShop;
    private GameObject currMenu;

    public WorldMapManager manager;
    public ShopsDetailsPanel buyShopPanel;
    public ShopSellPanelList sellPanel;
    public TMP_Text ShopTitle;
    public TMP_Text ShopDescription;
    public TMP_Text CreditsCurrentlyHave;

    public void InitMenu(Shop currShop)
    {
        manager.worldMapInput.SwitchState(new BlockUserInputState());

        this.currShop = currShop;
        gameObject.SetActive(true);


        ShopTitle.text = currShop.ShopName;
        ShopDescription.text = currShop.Description;
        PrintCredits();
        // DisplayBuyMenu();
        manager.worldMapUIManager.ToggleOffWolrdMapInfo();

    }

    public void PrintCredits()
    {
        CreditsCurrentlyHave.text = "Credits avaliable: " + Globals.campaign.currentparty.Credits.ToString();

    }

    public void ToggleOffMenu()
    {
        gameObject.SetActive(false);
    }

    public void CloseShop()
    {
        currShop = null;
        ToggleOffMenu();

        buyShopPanel.gameObject.SetActive(false);
        sellPanel.gameObject.SetActive(false);

        manager.worldMapUIManager.ToggleOnWolrdMapInfo();
    }

    public void DisplayBuyMenu()
    {
        ToggleOffMenu();

        ToggleOnPanel(buyShopPanel.gameObject);
        buyShopPanel.ShopSelected(currShop);

    }



    public void SellButton()
    {
        ToggleOffMenu();

        ToggleOnPanel(sellPanel.gameObject);
        sellPanel.ShopSelected(currShop);
    }

    public void BackOutofMenuButton()
    {
        InitMenu(currShop);
        currMenu.gameObject.SetActive(false);
    }

    public void ToggleOnPanel(GameObject panel)
    {
        if (currMenu != null)
        {
            currMenu.SetActive(false);
        }

        currMenu = panel;
    }


}

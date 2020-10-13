using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopsListPanel : MonoBehaviour
{
    //prefabs
    public Button ShopButtonPrefab;

    //ui
    public ShopMenuPanel shopMenuPanel;
    public Transform ButtonContainer;
    public ShopsDetailsPanel shopdeets;
    List<Button> ShopButtons;

    //vars
    ShopContainer shopConatiner;


	public void InitShopList(ShopContainer shopConatiner)
    {
        gameObject.SetActive(true);
        this.shopConatiner = shopConatiner;

        PopulateList();
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);

    }

    void PopulateList()
    {
        List<Shop> shops = shopConatiner.GetAvaliableShops();

        ClearList();

        foreach (Shop shop in shops)
        {
            CreateShopButton(shop);
        }
    }

    private void CreateShopButton(Shop shop)
    {
        Button button = Instantiate<Button>(ShopButtonPrefab, ButtonContainer);
        button.onClick.AddListener(delegate { ToggleOnShopDetails(shop); });
        button.transform.GetChild(0).GetComponent<Text>().text = shop.ShopName;
        ShopButtons.Add(button);
    }

    void ClearList()
    {
        if(ShopButtons == null)
        {
            ShopButtons = new List<Button>();
            return;
        }

        for (int i = ShopButtons.Count - 1; i >= 0; i--)
        {
            ShopButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Destroy(ShopButtons[i]);
            GameObject.Destroy(ShopButtons[i].gameObject);
        }

        ShopButtons = new List<Button>();
    }

    void ToggleOnShopDetails(Shop shop)
    {
        shopMenuPanel.InitMenu(shop);
    }

    private void OnDisable()
    {
        ClearList();
        shopdeets.gameObject.SetActive(false);
    }
}

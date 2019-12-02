using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{

    public static Shop instance;
    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;
    public Text goldText;
    public ItemButtons[] buyItemButtons;
    public ItemButtons[] sellItemButtons;
    public string[] itemsForSale;

    public Item selectedItem;
    private int selectedItemQuantity;
    public Text buyItemName, buyItemDesc, buyItemValue;
    public Text sellItemName, sellItemDesc, sellItemValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();
        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "G";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);

        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press();
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }


    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        ShowSellItem();
    }

    private void ShowSellItem()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem)
    {
        if (buyItem == null)
        {
            buyItemName.text = "";
            buyItemDesc.text = "";
            buyItemValue.text = "Cost: " + 0;
        }
        else
        {
            selectedItem = buyItem;
            buyItemName.text = selectedItem.itemName;
            buyItemDesc.text = selectedItem.description;
            buyItemValue.text = "Value " + selectedItem.value + "G";
        }
    }
    public void SelectSellItem(Item sellItem, int sellItemQuantity)
    {
        if (sellItem != null)
        {
            selectedItem = sellItem;
            selectedItemQuantity = sellItemQuantity;
            sellItemName.text = selectedItem.itemName;
            sellItemDesc.text = selectedItem.description;
            sellItemValue.text = "Value " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "G";
        }
    }

    public void buyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);

            }
        }
        goldText.text = GameManager.instance.currentGold.ToString() + "G";
    }

    public void sellItem()
    {
        if (selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName);

            selectedItemQuantity--;
            if (selectedItemQuantity <= 0)
            {
                selectedItem = null;
            }

            goldText.text = GameManager.instance.currentGold.ToString() + "g";

            ShowSellItem();
        }
    }
}


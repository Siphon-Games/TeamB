using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Currency<int> playerGold;

    [SerializeField]
    private Currency<int> playerSteel;

    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField]
    private Transform playerUIParent;

    [SerializeField]
    private GameObject itemUIPrefab;

    [SerializeField]
    private Transform itemUIParent;

    [SerializeField]
    private List<SomeItem<int>> items;

    private TextMeshProUGUI playerGoldState;
    private TextMeshProUGUI playerSteelState;

    private void Start()
    {
        InstantiatePlayer();
        InstantiateItems();
    }

    private void InstantiatePlayer()
    {
        TextMeshProUGUI playerGoldText = playerUIParent
            .Find("GoldText")
            .GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI playerSteelText = playerUIParent
            .Find("SteelText")
            .GetComponent<TextMeshProUGUI>();

        playerGoldText.text = playerGold.value.ToString() + " gold";
        playerSteelText.text = playerSteel.value.ToString() + " steel";

        playerGoldState = playerGoldText;
        playerSteelState = playerSteelText;
    }

    private void InstantiateItems()
    {
        foreach (var item in items)
        {
            Transform viewPort = itemUIParent.Find("Vertical").Find("Horizontal");
            GameObject itemUI = Instantiate(itemUIPrefab, viewPort);

            // Find and set the item name
            TextMeshProUGUI itemNameText = itemUI
                .transform.Find("Title")
                .GetComponent<TextMeshProUGUI>();
            itemNameText.text = item.itemName;

            // Find and set the item cost
            TextMeshProUGUI itemCostText = itemUI
                .transform.Find("Price")
                .GetComponent<TextMeshProUGUI>();
            string priceDisplay = "";

            foreach (var currencyValue in item.currencyValues)
            {
                priceDisplay +=
                    currencyValue.Key.ToString() + ": " + currencyValue.Value.ToString() + " ";
            }

            itemCostText.text = priceDisplay;

            // Randomize the item's color
            Image itemImage = itemUI.GetComponent<Image>();
            if (itemImage != null)
            {
                itemImage.color = GetRandomColor();
            }
        }
    }

    public void Buy()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        playerGoldState.text = "HELLO";
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}

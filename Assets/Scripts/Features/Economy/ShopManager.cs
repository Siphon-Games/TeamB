using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private Dictionary<SomeItem<int>, GameObject> itemUIMap =
        new Dictionary<SomeItem<int>, GameObject>();
    private List<SomeItem<int>> selectedItems = new List<SomeItem<int>>();

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

            foreach (var currencies in item.currencies)
            {
                CurrencyType type = currencies.currencyType;
                string itemValue = ((IValue<int>)item).GetValue(type).ToString();
                string itemCurrencyType = type.ToString().ToLower().FirstCharacterToUpper();

                priceDisplay += itemValue + " " + itemCurrencyType + " ";
            }

            itemCostText.text = priceDisplay.Trim();

            // Randomize the item's color
            Image itemImage = itemUI.GetComponent<Image>();
            if (itemImage != null)
            {
                itemImage.color = GetRandomColor();
            }

            // Store the mapping between the item and its UI representation
            itemUIMap[item] = itemUI;

            // Add a button component to detect click events
            Button itemButton = itemUI.GetComponent<Button>();
            itemButton = itemUI.AddComponent<Button>();

            itemButton.onClick.AddListener(() => OnItemClicked(item));
        }
    }

    public void Buy()
    {
        if (CanAffordSelectedItems())
        {
            PurchaseSelectedItems();
            UpdateUI();
        }
        else
        {
            messageText.text = "You're broke!";
        }
    }

    private bool CanAffordSelectedItems()
    {
        (int totalGold, int totalSteel, _) = CalculateTotals();
        return playerGold.CanAfford(new Currency<int>(CurrencyType.GOLD, totalGold))
            && playerSteel.CanAfford(new Currency<int>(CurrencyType.STEEL, totalSteel));
    }

    private void PurchaseSelectedItems()
    {
        (int totalGold, int totalSteel, _) = CalculateTotals();
        playerGold.value -= totalGold;
        playerSteel.value -= totalSteel;
    }

    private void UpdateUI()
    {
        (int totalGold, int totalSteel, string itemNames) = CalculateTotals();
        UpdateMessageBox(itemNames, totalGold, totalSteel);
        UpdatePlayerInfo();
    }

    private void UpdateMessageBox(string itemNames, int totalGold, int totalSteel)
    {
        string message;
        if (selectedItems.Count > 0)
        {
            message =
                $"Selected items: {itemNames}\n"
                + $"Total cost: {totalGold} gold{(totalSteel > 0 ? ", " + totalSteel + " steel" : "")}";
        }
        else
        {
            message = "No items selected";
        }

        messageText.text = message;
    }

    private void UpdatePlayerInfo()
    {
        playerGoldState.text = playerGold.value.ToString() + " gold";
        playerSteelState.text = playerSteel.value.ToString() + " steel";
    }

    private void OnItemClicked(SomeItem<int> item)
    {
        if (selectedItems.Contains(item))
        {
            DeselectItem(item);
        }
        else
        {
            SelectItem(item);
        }
    }

    private void SelectItem(SomeItem<int> item)
    {
        selectedItems.Add(item);

        // Update the item's color
        GameObject itemUI = itemUIMap[item];
        TextMeshProUGUI itemNameText = itemUI
            .transform.Find("Title")
            .GetComponent<TextMeshProUGUI>();
        itemNameText.color = Color.yellow;
    }

    private void DeselectItem(SomeItem<int> item)
    {
        selectedItems.Remove(item);

        // Update the item's color
        GameObject itemUI = itemUIMap[item];
        TextMeshProUGUI itemNameText = itemUI
            .transform.Find("Title")
            .GetComponent<TextMeshProUGUI>();
        itemNameText.color = Color.black;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private (int totalGold, int totalSteel, string itemNames) CalculateTotals()
    {
        int totalGold = 0;
        int totalSteel = 0;
        string itemNames = "";

        foreach (var item in selectedItems)
        {
            totalGold += ((IValue<int>)item).GetValue(CurrencyType.GOLD);
            totalSteel += ((IValue<int>)item).GetValue(CurrencyType.STEEL);
            itemNames += item.itemName + ", ";
        }

        // Remove the trailing comma and space
        if (itemNames.Length > 0)
        {
            itemNames.Substring(0, itemNames.Length - 2);
        }

        return (totalGold, totalSteel, itemNames);
    }
}

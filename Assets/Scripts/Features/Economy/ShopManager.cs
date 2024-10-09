using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the shop functionality for the MerchantTestScene.
/// This class is responsible for handling player currency, item selection, and purchase operations.
/// It is intended for testing purposes only and should not be used in production.
/// </summary>
public class ShopManager : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private CurrencyManager currencyManager;

    [SerializeField]
    private int initialPlayerGold = 100;

    [SerializeField]
    private int initialPlayerSteel = 50;

    [SerializeField]
    private TextMeshProUGUI playerGoldText;

    [SerializeField]
    private TextMeshProUGUI playerSteelText;

    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField]
    private GameObject itemUIPrefab;

    [SerializeField]
    private Transform itemUIParent;

    [SerializeField]
    private List<CurrencyItem<int>> items;

    #endregion

    #region Private Fields

    private Currency<int> playerGold;
    private Currency<int> playerSteel;
    private Dictionary<CurrencyItem<int>, GameObject> itemUIMap =
        new Dictionary<CurrencyItem<int>, GameObject>();
    private List<CurrencyItem<int>> selectedItems = new List<CurrencyItem<int>>();

    #endregion

    #region Unity Lifecycle

    /// <summary>
    /// Initializes the player's currency and instantiates shop items.
    /// </summary>
    private void Start()
    {
        InstantiatePlayer();
        InstantiateItems();
    }

    #endregion

    #region Initialization Methods


    /// <summary>
    /// Sets up the player's initial currency values and updates the UI.
    /// </summary>
    private void InstantiatePlayer()
    {
        CurrencyTypeSO goldType = currencyManager.GetCurrencyType("Gold");
        CurrencyTypeSO steelType = currencyManager.GetCurrencyType("Steel");

        playerGold = new Currency<int>(goldType, initialPlayerGold);
        playerSteel = new Currency<int>(steelType, initialPlayerSteel);

        playerGoldText.text = playerGold.value.ToString() + " gold";
        playerSteelText.text = playerSteel.value.ToString() + " steel";
    }

    /// <summary>
    /// Creates UI elements for each shop item and sets up click listeners.
    /// </summary>
    private void InstantiateItems()
    {
        Transform viewPort = itemUIParent.Find("Vertical").Find("Horizontal");
        var goldType = currencyManager.GetCurrencyType("Gold");
        var steelType = currencyManager.GetCurrencyType("Steel");

        foreach (var item in items)
        {
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

            int goldValue = item.GetValue(goldType);
            int steelValue = item.GetValue(steelType);

            if (goldValue > 0)
            {
                priceDisplay += $"{goldValue} Gold ";
            }
            if (steelValue > 0)
            {
                priceDisplay += $"{steelValue} Steel";
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

    #endregion

    #region Public Methods

    /// <summary>
    /// Attempts to purchase the selected items if the player has sufficient funds.
    /// Updates the UI to reflect the transaction or displays an error message.
    /// </summary>
    public void Buy()
    {
        if (CanAffordSelectedItems())
        {
            PurchaseSelectedItems();
            UpdateUI();
        }
        else
        {
            messageText.text = "You're too broke to buy make this purchase!";
        }
    }

    #endregion

    #region Private Helper Methods

    /// <summary>
    /// Checks if the player has enough currency to purchase all selected items.
    /// </summary>
    /// <returns>True if the player can afford the items, false otherwise.</returns>
    private bool CanAffordSelectedItems()
    {
        (int totalGold, int totalSteel, _) = CalculateTotals();
        return playerGold.CanAfford(totalGold) && playerSteel.CanAfford(totalSteel);
    }

    /// <summary>
    /// Deducts the cost of selected items from the player's currency.
    /// </summary>
    private void PurchaseSelectedItems()
    {
        (int totalGold, int totalSteel, _) = CalculateTotals();
        playerGold.value -= totalGold;
        playerSteel.value -= totalSteel;
    }

    /// <summary>
    /// Updates all UI elements to reflect the current state of the shop and player currency.
    /// </summary>
    private void UpdateUI()
    {
        (int totalGold, int totalSteel, string itemNames) = CalculateTotals();
        UpdateMessageBox(itemNames, totalGold, totalSteel);
        UpdatePlayerInfo();
    }

    /// <summary>
    /// Updates the message box with information about selected items and their total cost.
    /// </summary>
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

    /// <summary>
    /// Updates the display of the player's current currency values.
    /// </summary>
    private void UpdatePlayerInfo()
    {
        playerGoldText.text = playerGold.value.ToString() + " gold";
        playerSteelText.text = playerSteel.value.ToString() + " steel";
    }

    /// <summary>
    /// Handles the selection or deselection of an item when clicked.
    /// </summary>
    /// <param name="item">The item that was clicked.</param>
    private void OnItemClicked(CurrencyItem<int> item)
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

    /// <summary>
    /// Adds an item to the selected items list and updates its UI to show selection.
    /// </summary>
    /// <param name="item">The item to select.</param>
    private void SelectItem(CurrencyItem<int> item)
    {
        selectedItems.Add(item);

        // Update the item's color
        GameObject itemUI = itemUIMap[item];
        TextMeshProUGUI itemNameText = itemUI
            .transform.Find("Title")
            .GetComponent<TextMeshProUGUI>();
        itemNameText.color = Color.yellow;
    }

    /// <summary>
    /// Removes an item from the selected items list and updates its UI to show deselection.
    /// </summary>
    /// <param name="item">The item to deselect.</param>
    private void DeselectItem(CurrencyItem<int> item)
    {
        selectedItems.Remove(item);

        // Update the item's color
        GameObject itemUI = itemUIMap[item];
        TextMeshProUGUI itemNameText = itemUI
            .transform.Find("Title")
            .GetComponent<TextMeshProUGUI>();
        itemNameText.color = Color.black;
    }

    /// <summary>
    /// Generates a random color for item UI elements.
    /// </summary>
    /// <returns>A random Color object.</returns>
    private Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    /// <summary>
    /// Calculates the total cost of all selected items and generates a list of their names.
    /// </summary>
    /// <returns>A tuple containing the total gold cost, total steel cost, and a comma-separated list of item names.</returns>
    private (int totalGold, int totalSteel, string itemNames) CalculateTotals()
    {
        int totalGold = 0;
        int totalSteel = 0;
        string itemNames = "";

        foreach (var item in selectedItems)
        {
            CurrencyTypeSO goldType = currencyManager.GetCurrencyType("Gold");
            CurrencyTypeSO steelType = currencyManager.GetCurrencyType("Steel");

            totalGold += ((IValue<int>)item).GetValue(goldType);
            totalSteel += ((IValue<int>)item).GetValue(steelType);
            itemNames += item.itemName + ", ";
        }

        // Remove the trailing comma and space
        if (itemNames.Length > 0)
        {
            itemNames.Substring(0, itemNames.Length - 2);
        }

        return (totalGold, totalSteel, itemNames);
    }

    #endregion
}

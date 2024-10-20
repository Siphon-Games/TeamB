using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image recipeImage;

    [SerializeField]
    private TextMeshProUGUI ingredientNameText;

    [SerializeField]
    private TextMeshProUGUI quantityOwnedText;

    [SerializeField]
    private TextMeshProUGUI quantityNeededText;

    private RecipeSO recipe;
    private InventoryUI inventory;

    public void Initialize(RecipeSO recipe, InventoryUI inventory)
    {
        this.recipe = recipe;
        this.inventory = inventory;

        recipeImage.sprite = recipe.ResultingDish.Icon;
        recipeImage.enabled = true;

        UpdateIngredientUI();
        UpdateQuantityOwnedText();
    }

    private void UpdateIngredientUI()
    {
        StringBuilder ingredientsInfo = new StringBuilder();
        StringBuilder quantityNeededInfo = new StringBuilder();

        foreach (var ingredient in recipe.Ingredients)
        {
            ingredientsInfo.AppendLine(ingredient.item.Name);
            quantityNeededInfo.AppendLine($"/{ingredient.quantityNeeded}");
        }

        ingredientNameText.text = ingredientsInfo.ToString().TrimEnd('\n');
        quantityNeededText.text = quantityNeededInfo.ToString().TrimEnd('\n');
    }

    private void UpdateQuantityOwnedText()
    {
        StringBuilder quantityOwnedInfo = new StringBuilder();

        foreach (var ingredient in recipe.Ingredients)
        {
            var inventoryItem = inventory.items.FirstOrDefault(item =>
                item.Item == ingredient.item
            );

            quantityOwnedInfo.AppendLine(
                inventoryItem != null ? inventoryItem.Quantity.ToString() : "0"
            );
        }

        quantityOwnedText.text = quantityOwnedInfo.ToString().TrimEnd('\n');
    }

    private bool HasEnoughIngredients()
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            var inventoryItem = inventory.items.FirstOrDefault(item =>
                item.Item == ingredient.item
            );

            if (inventoryItem == null || inventoryItem.Quantity < ingredient.quantityNeeded)
            {
                return false;
            }
        }

        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (HasEnoughIngredients())
        {
            InventoryItem newDish = new InventoryItem(recipe.ResultingDish, 1);
            inventory.AddNewItem(newDish, 1);
        }
        else
        {
            Debug.Log("Not enough");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeItem : MonoBehaviour, IPointerClickHandler
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

    public void Initialize(RecipeSO recipe)
    {
        this.recipe = recipe;
        recipeImage.sprite = recipe.ResultingDish.Icon;
        recipeImage.enabled = true;

        UpdateIngredientUI();
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

    public void UpdateQuantityOwnedText(InventoryUI inventory)
    {
        StringBuilder quantityOwnedInfo = new StringBuilder();

        List<InventoryItem> matchingItems = inventory.items.FindAll(item =>
            recipe.Ingredients.Any(ingredient => ingredient.item == item.item)
        );

        foreach (var item in matchingItems)
        {
            quantityOwnedInfo.AppendLine(item.Quantity.ToString());
        }

        quantityOwnedText.text = quantityOwnedInfo.ToString().TrimEnd('\n');
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Recipe clicked: {recipe.ResultingDish.Name}");
    }
}

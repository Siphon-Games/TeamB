using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField]
    private RecipeItem recipeItemPrefab;

    [SerializeField]
    private Transform recipeItemParent;

    [SerializeField]
    private List<RecipeSO> recipes;

    private void Start()
    {
        LoadRecipes();
    }

    private void LoadRecipes()
    {
        // Set initial position
        Vector2 position = new Vector2(0, 98);

        foreach (var recipe in recipes)
        {
            RecipeItem recipeItem = Instantiate(recipeItemPrefab, recipeItemParent);

            RectTransform rectTransform = recipeItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            position.y -= rectTransform.rect.height + 10f;

            recipeItem.Initialize(recipe);
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image recipeImage;

    [SerializeField]
    private TextMeshProUGUI recipeGuideText;

    private RecipeSO recipe;

    public void Initialize(RecipeSO recipe)
    {
        this.recipe = recipe;
        recipeImage.sprite = recipe.ResultingDish.Icon;
        recipeImage.enabled = true;

        var ingredientsText = "";
        foreach (var ingredient in recipe.Ingredients)
        {
            ingredientsText += $"{ingredient.item.Name} 0/{ingredient.quantity}\n";
        }

        recipeGuideText.text = ingredientsText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Recipe clicked: {recipe.ResultingDish.Name}");
    }
}

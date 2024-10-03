using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    private void OnEnable()
    {
        List<int> myList = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var randomItemGiver = new ItemGiver<int>(myList);
        List<ChanceItem> itemsWithChance = new() { new("rocks", 50), new("nice rocks", 30), new("nicer rocks", 20), new("fridgin diamonds man!", 1) };
        var weightedrandomItemGiver = new WeightedItemGiver<ChanceItem>(itemsWithChance);

        randomItemGiver.GetRandomItem();
        weightedrandomItemGiver.GetWeightedRandomItem();
    }
}

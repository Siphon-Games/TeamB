using System.Collections.Generic;
using UnityEngine;

public class WeightedItem : IChanceScore
{
    // This is for testing purposes only:
    public static List<WeightedItem> weightedItems = new List<WeightedItem>
    {
        // Common items (70% total)
        new WeightedItem("Common - Rusty Dagger", 20f),
        new WeightedItem("Common - Wooden Shield", 15f),
        new WeightedItem("Common - Leather Boots", 20f),
        new WeightedItem("Common - Health Potion", 15f),
        // Uncommon items (20% total)
        new WeightedItem("Uncommon - Silver Sword", 8f),
        new WeightedItem("Uncommon - Elven Bow", 7f),
        new WeightedItem("Uncommon - Mithril Chainmail", 5f),
        // Rare items (9% total)
        new WeightedItem("Rare - Dragonscale Armor", 3f),
        new WeightedItem("Rare - Wand of Fireballs", 3f),
        new WeightedItem("Rare - Boots of Swiftness", 3f),
        // Legendary items (1% total)
        new WeightedItem("Legendary - Excalibur", 0.5f),
        new WeightedItem("Legendary - The One Ring", 0.5f),
    };

    public float chanceScore { get; }
    public string itemName { get; }

    public WeightedItem(string itemName, float chanceScore)
    {
        this.chanceScore = chanceScore;
        this.itemName = itemName;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class WeightedItem : IChanceScore
{
    public float chanceScore { get; }
    public string itemName { get; }

    public WeightedItem(string itemName, float chanceScore)
    {
        this.chanceScore = chanceScore;
        this.itemName = itemName;
    }
}

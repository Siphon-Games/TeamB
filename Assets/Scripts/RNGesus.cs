using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

/// <summary>
/// This class provides two main methods:
/// - Roll: Returns a random item from a list based on regular distribution.
/// - WeightedRoll: Returns a random item from a list based on weighted distribution.
///
/// Both methods require the input list to implement the IChanceScore interface.
/// </summary>
public static class RNGesus
{
    public static T Roll<T>(IList<T> items)
    {
        CheckValidity(items);
        return items[Random.Range(0, items.Count)];
    }

    public static T WeightedRoll<T>(IList<T> items)
        where T : IChanceScore
    {
        CheckValidity(items);

        float totalScore = items.Sum(item => item.chanceScore);
        float randomValue = Random.Range(0f, totalScore);
        float currentScore = 0f;

        foreach (T item in items)
        {
            currentScore += item.chanceScore;

            if (currentScore > randomValue)
            {
                return item;
            }
        }

        // This is a fallback to ensure that the method always returns a value.
        return items[Random.Range(0, items.Count)];
    }

    private static void CheckValidity<T>(IList<T> items)
    {
        if (items == null || items.Count == 0)
        {
            throw new ArgumentException("The collection cannot be null or empty");
        }
    }
}

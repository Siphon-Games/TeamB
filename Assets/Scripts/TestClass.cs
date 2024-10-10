using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    void Start()
    {
        TestRegularRoll();
        TestWeightedRoll();
    }

    private void TestRegularRoll()
    {
        List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<string> stringList = new List<string> { "Hello", "World", "This", "Is", "A", "Test" };
        var randomNumber = RNGesus.Roll(list);
        var randomString = RNGesus.Roll(stringList);

        Debug.Log($"Regular RNGesus - Random Number: {randomNumber}");
        Debug.Log($"Regular RNGesus - Random String: {randomString}");
    }

    private void TestWeightedRoll()
    {
        // Run multiple times to demonstrate weight distribution
        int trials = 100;

        for (int i = 0; i < trials; i++)
        {
            var result = RNGesus.WeightedRoll(WeightedItem.weightedItems);
            Debug.Log($"Weighted RNGesus - Random Item: {result.itemName}");
        }
    }
}

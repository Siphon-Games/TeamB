using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an item that can be purchased with multiple currencies.
/// This class allows for querying the item's cost in various currencies and applying discounts.
/// </summary>
[Serializable]
public class CurrencyItem : IValue
{
    [field: SerializeField]
    public List<Currency<int>> currencies { get; private set; } = new List<Currency<int>>();

    public float discountedValue;

    public ItemSO item;

    public int GetValue(CurrencyTypeSO currencyType)
    {
        foreach (var currency in currencies)
        {
            if (currency.currencyType.Equals(currencyType))
            {
                if (discountedValue > 0)
                {
                    return currency.GetDiscounted(discountedValue).value;
                }

                return currency.value;
            }
        }

        return default;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an item that can be purchased with multiple currencies.
/// The generic type <typeparamref name="T"/> represents the value type of each currency (e.g., <see cref="int"/> or <see cref="float"/>).
/// This class allows for querying the item's cost in various currencies and applying discounts.
/// </summary>
/// <typeparam name="T">The type of the currency value, constrained to value types (<see cref="struct"/>) that implement <see cref="IComparable{T}"/>.</typeparam>
[Serializable]
public class CurrencyItem<T> : IValue<T>
    where T : struct, IComparable<T>
{
    [field: SerializeField]
    public List<Currency<T>> currencies { get; private set; } = new List<Currency<T>>();

    public float discountedValue;

    [field: SerializeField]
    public string itemName { get; private set; }

    /// <summary>
    /// Retrieves the value of the item in a specific currency.
    /// </summary>
    /// <param name="currencyType">The type of currency (e.g., gold, steel).</param>
    /// <returns>
    /// The item's value in the specified currency. If a discount is applied, returns the discounted value.
    /// If the currency type is not found, returns the default value of <typeparamref name="T"/>.
    /// </returns>
    public T GetValue(CurrencyTypeSO currencyType)
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

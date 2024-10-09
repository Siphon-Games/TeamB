using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CurrencyItem<T> : IValue<T>
    where T : struct, IComparable<T>
{
    [field: SerializeField]
    public List<Currency<T>> currencies { get; private set; } = new List<Currency<T>>();

    public float discountedValue;

    // Make this into an SO? with description, name and sprite?
    [field: SerializeField]
    public string itemName { get; private set; }

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

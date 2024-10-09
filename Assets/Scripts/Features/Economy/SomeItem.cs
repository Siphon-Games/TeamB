using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SomeItem<T> : IValue<T>
    where T : struct, IComparable<T>
{
    [SerializeField]
    public List<Currency<T>> currencies;

    [SerializeField]
    public float discountedValue;

    [SerializeField]
    public string itemName;

    public SomeItem(string itemName, float discountedValue)
    {
        this.itemName = itemName;
        this.discountedValue = discountedValue;
    }

    T IValue<T>.GetValue(CurrencyType currencyType)
    {
        foreach (var currency in currencies)
        {
            if (currency.currencyType == currencyType)
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

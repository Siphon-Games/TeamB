using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public class SomeItem<T> : IValue<T>
    where T : struct
{
    [SerializedDictionary("Currency Values", "Currency Values")]
    public SerializedDictionary<CurrencyType, T> currencyValues;

    [SerializeField]
    public string itemName;

    public SomeItem(string itemName)
    {
        this.itemName = itemName;
    }

    public void SetValue(CurrencyType currencyType, T value)
    {
        currencyValues[currencyType] = value;
    }

    public T GetValue(CurrencyType currencyType)
    {
        if (currencyValues.TryGetValue(currencyType, out T value))
        {
            return value;
        }

        return default;
    }
}

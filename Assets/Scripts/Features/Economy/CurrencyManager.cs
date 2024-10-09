using System;
using System.Collections.Generic;

/// <summary>
/// Manages the different types of currencies available in the game.
/// This class is responsible for storing and retrieving currency types.
/// </summary>
[Serializable]
public class CurrencyManager
{
    public List<CurrencyTypeSO> availableCurrencies;

    public CurrencyTypeSO GetCurrencyType(string currencyName)
    {
        return availableCurrencies.Find(c => c.currency == currencyName);
    }

    public List<string> GetAvailableCurrencyNames()
    {
        List<string> result = new List<string>();

        foreach (var c in availableCurrencies)
        {
            result.Add(c.currency);
        }

        return result;
    }
}

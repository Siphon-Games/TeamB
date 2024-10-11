using System;
using System.Collections.Generic;

/// <summary>
/// Manages all the different types of currencies available in the game.
/// This class is responsible for storing, managing, and retrieving currency types, allowing other systems
/// to query currency details based on their name or retrieve a list of available currencies.
/// </summary>
[Serializable]
public class CurrencyManager
{
    public List<CurrencyTypeSO> availableCurrencies;

    public CurrencyTypeSO GetCurrencyType(string currencyName)
    {
        return availableCurrencies.Find(c => c.currency == currencyName);
    }
}

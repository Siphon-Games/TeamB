using System;

/// <summary>
/// Generic class for currency.
/// Struct helps with immutability, performance, and compatibility with math operations.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class Currency<T>
    where T : struct, IComparable<T>
{
    public CurrencyType currencyType;
    public T value;

    public Currency(CurrencyType currencyType, T value)
    {
        this.currencyType = currencyType;
        this.value = value;
    }

    public bool CanAfford(Currency<T> cost)
    {
        // Check if the currency types are the same
        if (currencyType != cost.currencyType)
            return false;

        return value.CompareTo(cost.value) >= 0;
    }

    public Currency<T> GetDiscounted(float discountValue)
    {
        // Dyanmic can incur a small performance overhead, but assuming
        // this is not a frequent operation, it "should" be fine
        dynamic currentValue = value;
        dynamic discountedValue = currentValue * (1 - discountValue);
        return new Currency<T>(currencyType, (T)discountedValue);
    }
}
